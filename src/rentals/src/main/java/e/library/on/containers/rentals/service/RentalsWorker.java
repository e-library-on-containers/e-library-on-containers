package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.repository.RentalsEventRepository;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import e.library.on.containers.rentals.utils.events.BookExtendedEvent;
import e.library.on.containers.rentals.utils.events.BookRentedEvent;
import e.library.on.containers.rentals.utils.events.BookReturnedEvent;
import e.library.on.containers.rentals.utils.events.Event;
import e.library.on.containers.rentals.utils.events.MasstransitEventWrapper;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Component;
import org.springframework.transaction.annotation.Transactional;

@Slf4j
@Component
class RentalsWorker {
    private final RentalsReadRepository readRepository;
    private final RentalsEventRepository eventRepository;

    RentalsWorker(RentalsReadRepository readRepository, RentalsEventRepository eventRepository) {
        this.readRepository = readRepository;
        this.eventRepository = eventRepository;
    }

    @RabbitListener(queues = "${rabbitmq.rent-queue.name}")
    public void receiveRentMessage(MasstransitEventWrapper<BookRentedEvent> event) {
        log.info("Received: {}", event);
        updateProjection();
    }

    @RabbitListener(queues = "${rabbitmq.return-queue.name}")
    public void receiveReturnMessage(MasstransitEventWrapper<BookReturnedEvent> event) {
        log.info("Received: {}", event);
        updateProjection();
    }

    @RabbitListener(queues = "${rabbitmq.extend-queue.name}")
    public void receiveExtendMessage(MasstransitEventWrapper<BookExtendedEvent> event) {
        log.info("Received: {}", event);
        updateProjection();
    }

    @Transactional
    protected void updateProjection() {
        var lastModificationDate = readRepository.getLastModificationDate();
        var oldEvents = eventRepository.getAllEventsPast(lastModificationDate);
        int iterations = 0;
        for (Event oldEvent : oldEvents) {
            handle(oldEvent);
            iterations++;
        }
        readRepository.updateLastModificationDate();
        log.info("Updated projection with {} new event(s)", iterations);
    }

    private void handle(Event oldEvent) {
        if (oldEvent instanceof BookRentedEvent event) {
            var dao = RentalsReadDao.from(event);
            readRepository.insertRental(dao);
            return;
        }
        if (oldEvent instanceof BookReturnedEvent event) {
            var rentalId = event.getRentalId();
            boolean isRemoved = readRepository.removeRental(rentalId);
            if (!isRemoved) {
                log.info("There were no rental with id {}", rentalId);
            }
            return;
        }
        if (oldEvent instanceof BookExtendedEvent event) {
            var rentalId = event.getRentalId();
            var dao = readRepository.getRentalById(rentalId).orElseThrow().withExtendedRent(event.getDays());
            readRepository.updateRental(dao);
            return;
        }
        throw new IllegalArgumentException("Type of event received is not yet supported");

    }
}
