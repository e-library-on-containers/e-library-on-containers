package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.common.RentalEntityMapper;
import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;
import e.library.on.containers.rentals.events.Event;
import e.library.on.containers.rentals.exceptions.UnsupportedEventTypeException;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Component;

@Slf4j
@Component
@RequiredArgsConstructor
class RentalsWorker {
    private final RentalsReadRepository readRepository;
    private final RentalEntityMapper rentalEntityMapper = new RentalEntityMapper();

    @RabbitListener(queues = "${rabbitmq.rent-queue.name}")
    public void receiveRentMessage(BookRentedEvent event) {
        log(event);
        handle(event);
    }

    @RabbitListener(queues = "${rabbitmq.return-queue.name}")
    public void receiveReturnMessage(BookReturnedEvent event) {
        log(event);
        handle(event);
    }

    @RabbitListener(queues = "${rabbitmq.extend-queue.name}")
    public void receiveExtendMessage(BookExtendedEvent event) {
        log(event);
        handle(event);
    }

// TODO: Use below code as true projection
//    @Transactional
//    protected void updateProjection() {
//        var lastModificationDate = readRepository.findFirstByOrderByLastEditDateDesc()
//                .map(RentalEntity::getLastEditDate)
//                .orElse(ZonedDateTime.of(1900, 1, 1, 12, 0, 0, 0, ZoneId.of("UTC")));
//        var oldEvents = eventRepository.findAllByCreatedAtAfter(lastModificationDate).stream().map(eventEntityMapper::entityToEvent).toList();
//        int iterations = 0;
//        for (Event oldEvent : oldEvents) {
//            handle(oldEvent);
//            iterations++;
//        }
//        log.info("Updated projection with {} new event(s)", iterations);
//    }

    void handle(Event oldEvent) {
        if (oldEvent instanceof BookRentedEvent event) {
            final var entity = rentalEntityMapper.daoToEntity(RentalsReadDao.from(event));
            readRepository.save(entity);
            return;
        }
        if (oldEvent instanceof BookReturnedEvent event) {
            final var rentalId = event.getRentalId();
            readRepository.deleteById(rentalId.toString());
            return;
        }
        if (oldEvent instanceof BookExtendedEvent event) {
            final var rentalId = event.getRentalId();
            final var dao = readRepository.findById(rentalId.toString())
                    .map(rentalEntityMapper::entityToDao)
                    .map(rental -> rental.withExtendedRent(event.getDays()))
                    .orElseThrow();
            readRepository.save(rentalEntityMapper.daoToEntity(dao));
            return;
        }
        throw new UnsupportedEventTypeException(oldEvent.getClass());
    }

    private void log(Event event) {
        log.info("Received: {}", event);
    }
}
