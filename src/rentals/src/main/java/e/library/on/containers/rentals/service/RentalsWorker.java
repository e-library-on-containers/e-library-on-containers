package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.repository.RentalsEventRepository;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import e.library.on.containers.rentals.repository.entity.RentalEntity;
import e.library.on.containers.rentals.utils.RentalEntityMapper;
import e.library.on.containers.rentals.utils.events.BookExtendedEvent;
import e.library.on.containers.rentals.utils.events.BookRentedEvent;
import e.library.on.containers.rentals.utils.events.BookReturnedEvent;
import e.library.on.containers.rentals.utils.events.Event;
import e.library.on.containers.rentals.utils.events.MasstransitEventWrapper;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Component;
import org.springframework.transaction.annotation.Transactional;

import java.time.ZoneId;
import java.time.ZonedDateTime;

@Slf4j
@Component
@RequiredArgsConstructor
class RentalsWorker {
    private final RentalsReadRepository readRepository;
    private final RentalsEventRepository eventRepository;
    private final RentalEntityMapper rentalEntityMapper;

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
        var lastModificationDate = readRepository.findFirstByOrderByLastEditDateDesc()
                .map(RentalEntity::getLastEditDate)
                .orElse(ZonedDateTime.of(1900, 1, 1, 12, 0, 0, 0, ZoneId.of("UTC")));
        var oldEvents = eventRepository.getAllEventsPast(lastModificationDate);
        int iterations = 0;
        for (Event oldEvent : oldEvents) {
            handle(oldEvent);
            iterations++;
        }
        log.info("Updated projection with {} new event(s)", iterations);
    }

    private void handle(Event oldEvent) {
        if (oldEvent instanceof BookRentedEvent event) {
            var entity = rentalEntityMapper.daoToEntity(RentalsReadDao.from(event));
            readRepository.save(entity);
            return;
        }
        if (oldEvent instanceof BookReturnedEvent event) {
            var rentalId = event.getRentalId();
            readRepository.deleteById(rentalId.toString());
            return;
        }
        if (oldEvent instanceof BookExtendedEvent event) {
            var rentalId = event.getRentalId();
            var dao = readRepository.findById(rentalId.toString())
                    .map(rentalEntityMapper::entityToDao)
                    .map(x -> x.withExtendedRent(event.getDays()))
                    .orElseThrow();
            readRepository.save(rentalEntityMapper.daoToEntity(dao));
            return;
        }
        throw new IllegalArgumentException("Type of event received is not yet supported");

    }
}
