package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.common.EventEntityMapper;
import e.library.on.containers.rentals.events.AcceptBorrowEvent;
import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;
import e.library.on.containers.rentals.events.BorrowRequestEvent;
import e.library.on.containers.rentals.events.ReturnAwaitingApprovalEvent;
import e.library.on.containers.rentals.exceptions.RentDoesNotExistException;
import e.library.on.containers.rentals.exceptions.RentalForBookAlreadyExistsException;
import e.library.on.containers.rentals.exceptions.RentalForBookDoesntExistsException;
import e.library.on.containers.rentals.message.BorrowEventSender;
import e.library.on.containers.rentals.message.RentalEventSender;
import e.library.on.containers.rentals.repository.BorrowReadRepository;
import e.library.on.containers.rentals.repository.RentalsEventRepository;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.entity.RentalState;
import e.library.on.containers.rentals.web.entity.AcceptBorrowRequest;
import e.library.on.containers.rentals.web.entity.CreateBorrowRequest;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

@Slf4j
@RequiredArgsConstructor
@Service
public class RentalsService {
    private static final int DEFAULT_RENTAL_LENGTH = 30;

    private final RentalsEventRepository rentalsEventRepository;
    private final RentalsReadRepository readRepository;
    private final BorrowReadRepository borrowReadRepository;
    private final RentalEventSender rentalEventSender;
    private final BorrowEventSender borrowEventSender;

    private final EventEntityMapper eventEntityMapper = new EventEntityMapper();

    public UUID rentBook(UUID userId, int bookInstanceId) {
        readRepository.findByBookInstanceIdAndRentalStateNot(bookInstanceId, RentalState.RETURNED).ifPresent(
                entity -> {
                    throw new RentalForBookAlreadyExistsException(bookInstanceId);
                }
        );

        final var newRentalId = UUID.randomUUID();
        final var rentEvent = new BookRentedEvent(userId, bookInstanceId, newRentalId, DEFAULT_RENTAL_LENGTH);

        log.info("Renting book {}...", bookInstanceId);
        rentalsEventRepository.save(eventEntityMapper.eventToEntity(rentEvent));
        rentalEventSender.send(rentEvent);
        return rentEvent.getRentalId();
    }

    public void returnBook(UUID userId, UUID rentId) {
        final var rental = readRepository.findById(rentId)
                .orElseThrow(() -> new RentDoesNotExistException(rentId));
        final var returnEvent = new ReturnAwaitingApprovalEvent(rentId, userId, rental.getBookInstanceId());

        log.info("Returning book on rent with id {}...", rentId);
        rentalsEventRepository.save(eventEntityMapper.eventToEntity(returnEvent));
        rentalEventSender.send(returnEvent);
    }

    public void approveReturn(UUID rentId) {
        final var rental = readRepository.findById(rentId)
                .orElseThrow(() -> new RentDoesNotExistException(rentId));
        final var bookReturnedEvent = new BookReturnedEvent(rental.getUserId(), rental.getId(), rental.getBookInstanceId());

        log.info("Approved return of rent with id: {}", rental.getId());
        rentalsEventRepository.save(eventEntityMapper.eventToEntity(bookReturnedEvent));
        rentalEventSender.send(bookReturnedEvent);
    }

    public void extendRent(UUID userId, UUID rentId, int days) {
        if (readRepository.findById(rentId).isEmpty()) {
            throw new RentDoesNotExistException(rentId);
        }
        final var extendedEvent = new BookExtendedEvent(userId, rentId, days);

        log.info("Extending rental {} by {} days...", rentId, days);
        rentalsEventRepository.save(eventEntityMapper.eventToEntity(extendedEvent));
        rentalEventSender.send(extendedEvent);
    }

    public void borrow(UUID userId, CreateBorrowRequest createBorrowRequest) {
        final var bookInstanceId = createBorrowRequest.bookInstanceId();
        final var rental = readRepository.findByBookInstanceIdAndRentalStateNotIn(
                        bookInstanceId,
                        List.of(RentalState.RETURNED, RentalState.AWAITING_RETURN_APPROVAL)
                ).orElseThrow(() -> new RentalForBookDoesntExistsException(bookInstanceId));

        final var borrowRequestEvent = new BorrowRequestEvent(bookInstanceId, rental.getUserId(), userId);

        log.debug("User {} requested to borrow book {} from user {}...",
                borrowRequestEvent.getNewOwner(),
                borrowRequestEvent.getBookInstanceId(),
                borrowRequestEvent.getOriginalOwner()
        );
        borrowEventSender.send(borrowRequestEvent);
    }

    public void acceptBorrow(UUID userId, AcceptBorrowRequest acceptBorrowRequest) {
        final var borrowId = acceptBorrowRequest.borrowId();
        final var borrow = borrowReadRepository.findById(borrowId).orElseThrow(
                () -> new IllegalArgumentException("Borrow with given id (%s) doesn't exist!".formatted(borrowId))
        );

        if (!borrow.getPreviousOwner().equals(userId)) {
            throw new IllegalArgumentException("Previous owner should accept borrow request!");
        }

        final var rental = readRepository.findByBookInstanceIdAndRentalStateNot(borrow.getBookInstanceId(), RentalState.RETURNED).orElseThrow();
        rental.setRentalState(RentalState.RETURNED);
        readRepository.save(rental);

        final var borrowAcceptedEvent = new AcceptBorrowEvent(borrow.getId());
        borrowEventSender.send(borrowAcceptedEvent);
    }
}
