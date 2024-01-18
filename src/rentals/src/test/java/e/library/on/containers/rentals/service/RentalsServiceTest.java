package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.ReturnAwaitingApprovalEvent;
import e.library.on.containers.rentals.exceptions.RentDoesNotExistException;
import e.library.on.containers.rentals.exceptions.RentalForBookAlreadyExistsException;
import e.library.on.containers.rentals.message.BorrowEventSender;
import e.library.on.containers.rentals.message.RentalEventSender;
import e.library.on.containers.rentals.repository.BorrowReadRepository;
import e.library.on.containers.rentals.repository.RentalsEventRepository;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.entity.RentalEntity;
import e.library.on.containers.rentals.repository.entity.RentalState;
import org.junit.jupiter.api.Test;
import org.mockito.ArgumentCaptor;

import java.time.ZonedDateTime;
import java.util.Optional;
import java.util.UUID;

import static org.assertj.core.api.Assertions.assertThat;
import static org.assertj.core.api.Assertions.assertThatThrownBy;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

class RentalsServiceTest {

    private static final UUID TEST_USER_ID = UUID.randomUUID();
    private static final UUID TEST_RENT_ID = UUID.randomUUID();
    private static final int TEST_BOOK_INSTANCE_ID = 1;
    private static final int TEST_DAYS = 30;

    private final RentalsEventRepository rentalsEventRepository = mock(RentalsEventRepository.class);
    private final RentalsReadRepository rentalsReadRepository = mock(RentalsReadRepository.class);
    private final BorrowReadRepository borrowReadRepository = mock(BorrowReadRepository.class);
    private final RentalEventSender rentalEventSender = mock(RentalEventSender.class);
    private final BorrowEventSender borrowEventSender = mock(BorrowEventSender.class);
    private final RentalsService sut = new RentalsService(rentalsEventRepository, rentalsReadRepository, borrowReadRepository, rentalEventSender, borrowEventSender);

    @Test
    void givenBookInstance_WhenItIsNotRented_thenSentRentSuccessfully() {
        // GIVEN
        when(rentalsReadRepository.findByBookInstanceIdAndRentalStateNot(TEST_BOOK_INSTANCE_ID, RentalState.RETURNED)).thenReturn(Optional.empty());

        // WHEN
        sut.rentBook(TEST_USER_ID, TEST_BOOK_INSTANCE_ID);

        // THEN
        final var captor = ArgumentCaptor.forClass(BookRentedEvent.class);
        verify(rentalEventSender).send(captor.capture());
        assertThat(captor.getValue().getBookInstanceId()).isEqualTo(TEST_BOOK_INSTANCE_ID);
    }

    @Test
    void givenBookInstance_WhenItIsRented_thenThrowExceptionOnRenting() {
        // GIVEN
        final var borrowedBookRent = new RentalEntity();

        when(rentalsReadRepository.findByBookInstanceIdAndRentalStateNot(TEST_BOOK_INSTANCE_ID, RentalState.RETURNED)).thenReturn(Optional.of(borrowedBookRent));

        // WHEN-THEN
        assertThatThrownBy(() -> sut.rentBook(TEST_USER_ID, TEST_BOOK_INSTANCE_ID))
                .isInstanceOf(RentalForBookAlreadyExistsException.class)
                .hasMessage("Rental for given book (1) already exists!");
    }

    @Test
    void givenBookInstance_WhenItIsRented_thenReturnSuccessfully() {
        // GIVEN
        final var borrowedBookRent = RentalEntity.builder()
                .bookInstanceId(TEST_BOOK_INSTANCE_ID)
                .userId(TEST_USER_ID)
                .rentedAt(ZonedDateTime.now())
                .dueDate(ZonedDateTime.now().plusDays(30))
                .build();

        when(rentalsReadRepository.findById(TEST_RENT_ID)).thenReturn(Optional.of(borrowedBookRent));

        // WHEN
        sut.returnBook(TEST_USER_ID, TEST_RENT_ID);

        // THEN
        final var captor = ArgumentCaptor.forClass(ReturnAwaitingApprovalEvent.class);
        verify(rentalEventSender).send(captor.capture());
        assertThat(captor.getValue().getRentalId()).isEqualTo(TEST_RENT_ID);
        assertThat(captor.getValue().getBookInstanceId()).isEqualTo(TEST_BOOK_INSTANCE_ID);
    }

    @Test
    void givenBookInstance_WhenItIsNotRented_thenThrowExceptionOnReturning() {
        // GIVEN
        when(rentalsReadRepository.findById(TEST_RENT_ID)).thenReturn(Optional.empty());

        // WHEN-THEN
        assertThatThrownBy(() -> sut.returnBook(TEST_USER_ID, TEST_RENT_ID))
                .isInstanceOf(RentDoesNotExistException.class)
                .hasMessage("Rent with given ID (%s) doesn't exist!".formatted(TEST_RENT_ID));
    }

    @Test
    void givenBookInstance_WhenItIsRented_thenExtendingSuccessfully() {
        // GIVEN
        final var borrowedBookRent = RentalEntity.builder()
                .bookInstanceId(TEST_BOOK_INSTANCE_ID)
                .userId(TEST_USER_ID)
                .rentedAt(ZonedDateTime.now())
                .dueDate(ZonedDateTime.now().plusDays(30))
                .build();

        when(rentalsReadRepository.findById(TEST_RENT_ID)).thenReturn(Optional.of(borrowedBookRent));

        // WHEN
        sut.extendRent(TEST_USER_ID, TEST_RENT_ID, TEST_DAYS);

        // THEN
        final var captor = ArgumentCaptor.forClass(BookExtendedEvent.class);
        verify(rentalEventSender).send(captor.capture());
        assertThat(captor.getValue().getRentalId()).isEqualTo(TEST_RENT_ID);
        assertThat(captor.getValue().getDays()).isEqualTo(TEST_DAYS);
        assertThat(captor.getValue().getUserId()).isEqualTo(TEST_USER_ID);
    }

    @Test
    void givenBookInstance_WhenItIsNotRented_thenThrowExceptionOnExtending() {
        // GIVEN
        when(rentalsReadRepository.findById(TEST_RENT_ID)).thenReturn(Optional.empty());

        // WHEN-THEN
        assertThatThrownBy(() -> sut.extendRent(TEST_USER_ID, TEST_RENT_ID, TEST_DAYS))
                .isInstanceOf(RentDoesNotExistException.class)
                .hasMessage("Rent with given ID (%s) doesn't exist!".formatted(TEST_RENT_ID));
    }
}