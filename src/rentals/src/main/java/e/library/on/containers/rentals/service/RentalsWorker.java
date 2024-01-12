package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.common.RentalEntityMapper;
import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;
import e.library.on.containers.rentals.events.Event;
import e.library.on.containers.rentals.events.ReturnAwaitingApprovalEvent;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import e.library.on.containers.rentals.repository.entity.RentalState;
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
    public void handle(BookRentedEvent event) {
        log(event);
        final var entity = rentalEntityMapper.daoToEntity(RentalsReadDao.from(event));
        readRepository.save(entity);
    }

    @RabbitListener(queues = "${rabbitmq.awaiting-queue.name}")
    public void handle(ReturnAwaitingApprovalEvent event) {
        log(event);
        final var rentalId = event.getRentalId();
        final var dao = readRepository.findById(rentalId)
                .map(rentalEntityMapper::entityToDao)
                .map(rental -> rental.withState(RentalState.AWAITING_RETURN_APPROVAL))
                .orElseThrow();
        readRepository.save(rentalEntityMapper.daoToEntity(dao));
    }

    @RabbitListener(queues = "${rabbitmq.extend-queue.name}")
    public void handle(BookExtendedEvent event) {
        log(event);
        final var rentalId = event.getRentalId();
        final var dao = readRepository.findById(rentalId)
                .map(rentalEntityMapper::entityToDao)
                .map(rental -> rental.withExtendedRent(event.getDays()))
                .orElseThrow();
        readRepository.save(rentalEntityMapper.daoToEntity(dao));
    }

    @RabbitListener(queues = "${rabbitmq.return-queue.name}")
    public void handle(BookReturnedEvent event) {
        log(event);
        final var rentalId = event.getRentalId();
        final var dao = readRepository.findById(rentalId)
                .map(rentalEntityMapper::entityToDao)
                .map(rental -> rental.withState(RentalState.RETURNED))
                .orElseThrow();
        readRepository.save(rentalEntityMapper.daoToEntity(dao));
    }

    private void log(Event event) {
        log.info("Received: {}", event);
    }
}
