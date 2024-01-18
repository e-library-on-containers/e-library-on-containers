package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.common.RentalEntityMapper;
import e.library.on.containers.rentals.events.AcceptBorrowEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BorrowRequestEvent;
import e.library.on.containers.rentals.repository.BorrowReadRepository;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import e.library.on.containers.rentals.repository.entity.BorrowEntity;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Component;

import java.time.ZonedDateTime;
import java.util.UUID;

@Slf4j
@Component
@RequiredArgsConstructor
class BorrowWorker {
    private final BorrowReadRepository readRepository;
    private final RentalsReadRepository rentalsReadRepository;
    private final RentalEntityMapper rentalEntityMapper = new RentalEntityMapper();

    @RabbitListener(queues = "${rabbitmq.borrow-queue.name}")
    public void receiveBorrowMessage(BorrowRequestEvent event) {
        log.debug("Received borrow request event: {}", event);
        final var newBorrow = BorrowEntity.builder()
                .id(UUID.randomUUID())
                .newOwner(event.getNewOwner())
                .bookInstanceId(event.getBookInstanceId())
                .previousOwner(event.getOriginalOwner())
                .accepted(false)
                .lastEditDate(ZonedDateTime.now())
                .build();
        readRepository.save(newBorrow);
    }

    @RabbitListener(queues = "${rabbitmq.accept-queue.name}")
    public void receiveAcceptBorrowMessage(AcceptBorrowEvent event) {
        log.debug("Received borrow accepted event: {}", event);
        final var borrow = readRepository.findById(event.getBorrowId()).orElseThrow(
                () -> new IllegalArgumentException("Borrow with given id (%s) doesn't exists!".formatted(event.getBorrowId()))
        );
        borrow.setBorrowedAt(ZonedDateTime.now());
        borrow.setAccepted(true);
        final var newRentalId = UUID.randomUUID();
        final var createdRent = new BookRentedEvent(
                UUID.randomUUID(),
                ZonedDateTime.now(),
                borrow.getNewOwner(),
                newRentalId,
                borrow.getBookInstanceId(),
                7
        );
        final var createdRental = rentalEntityMapper.daoToEntity(RentalsReadDao.from(createdRent));
        rentalsReadRepository.save(createdRental);
        borrow.setCreatedRental(createdRental);
        readRepository.save(borrow);
    }
}
