package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.entity.RentalEntity;
import org.junit.jupiter.api.Test;
import org.mockito.ArgumentCaptor;

import java.util.Optional;
import java.util.UUID;

import static org.assertj.core.api.Assertions.assertThat;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.times;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

class RentalsWorkerTest {

    private final RentalsReadRepository repository = mock(RentalsReadRepository.class);
    private final RentalsWorker sut = new RentalsWorker(repository);

    @Test
    void givenBookRentedEvent_whenHandlingEvent_expectNewRentToBeCreated() {
        // GIVEN
        final var givenEvent = new BookRentedEvent(UUID.randomUUID(), 1, UUID.randomUUID(), 1);
        final var expectedRental = new RentalEntity(
                givenEvent.getRentalId().toString(),
                1,
                givenEvent.getUserId().toString(),
                givenEvent.getCreatedAt(),
                givenEvent.getCreatedAt().plusDays(1),
                false,
                null
        );

        // WHEN
        sut.handle(givenEvent);

        // THEN
        final var captor = ArgumentCaptor.forClass(RentalEntity.class);
        verify(repository, times(1)).save(captor.capture());
        assertThat(captor.getValue()).satisfies(entity -> {
            assertThat(entity.getId()).isEqualTo(expectedRental.getId());
            assertThat(entity.getBookInstanceId()).isEqualTo(expectedRental.getBookInstanceId());
            assertThat(entity.getUserId()).isEqualTo(expectedRental.getUserId());
            assertThat(entity.getRentedAt()).isEqualTo(expectedRental.getRentedAt());
            assertThat(entity.getDueDate()).isEqualTo(expectedRental.getDueDate());
            assertThat(entity.isExtended()).isEqualTo(expectedRental.isExtended());
        });
    }

    @Test
    void givenBookReturnedEvent_whenHandlingEvent_expectRentToBeRemoved() {
        // GIVEN
        final var givenEvent = new BookReturnedEvent(UUID.randomUUID(), UUID.randomUUID(), 1);
        final var testRentalId = givenEvent.getRentalId().toString();

        // WHEN
        sut.handle(givenEvent);

        // THEN
        verify(repository, times(1)).deleteById(testRentalId);
    }

    @Test
    void givenBookExtendedEvent_whenHandlingEvent_expectRentToBeExtended() {
        // GIVEN
        final var givenEvent = new BookExtendedEvent(UUID.randomUUID(), UUID.randomUUID(), 1);
        final var testRentalId = givenEvent.getRentalId().toString();
        final var expectedRental = new RentalEntity(
                givenEvent.getRentalId().toString(),
                1,
                givenEvent.getUserId().toString(),
                givenEvent.getCreatedAt(),
                givenEvent.getCreatedAt().plusDays(2),
                true,
                null
        );
        final var testEntity = new RentalEntity(
                givenEvent.getRentalId().toString(),
                1,
                givenEvent.getUserId().toString(),
                givenEvent.getCreatedAt(),
                givenEvent.getCreatedAt().plusDays(1),
                false,
                null
        );
        when(repository.findById(testRentalId)).thenReturn(Optional.of(testEntity));

        // WHEN
        sut.handle(givenEvent);

        // THEN
        final var captor = ArgumentCaptor.forClass(RentalEntity.class);
        verify(repository, times(1)).save(captor.capture());
        assertThat(captor.getValue()).satisfies(entity -> {
            assertThat(entity.getId()).isEqualTo(expectedRental.getId());
            assertThat(entity.getBookInstanceId()).isEqualTo(expectedRental.getBookInstanceId());
            assertThat(entity.getUserId()).isEqualTo(expectedRental.getUserId());
            assertThat(entity.getRentedAt()).isEqualTo(expectedRental.getRentedAt());
            assertThat(entity.getDueDate()).isEqualTo(expectedRental.getDueDate());
            assertThat(entity.isExtended()).isEqualTo(expectedRental.isExtended());
        });
    }
}
