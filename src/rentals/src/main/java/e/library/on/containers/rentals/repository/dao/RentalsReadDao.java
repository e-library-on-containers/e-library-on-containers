package e.library.on.containers.rentals.repository.dao;

import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.repository.entity.RentalState;

import java.time.ZonedDateTime;
import java.util.UUID;

public record RentalsReadDao(
		UUID id,
		int bookCopyId,
		UUID userId,
		ZonedDateTime rentedAt,
		ZonedDateTime dueDate,
		RentalState rentalState
) {
	public static RentalsReadDao from(BookRentedEvent event) {
		return new RentalsReadDao(
				event.getRentalId(),
				event.getBookInstanceId(),
				event.getUserId(),
				event.getCreatedAt(),
				event.getCreatedAt().plusDays(event.getForHowManyDays()),
				RentalState.ACTIVE
		);
	}

	public RentalsReadDao withExtendedRent(int days) {
		return new RentalsReadDao(
				id(),
				bookCopyId(),
				userId(),
				rentedAt(),
				dueDate().plusDays(days),
				RentalState.EXTENDED
		);
	}

	public RentalsReadDao withState(RentalState state) {
		return new RentalsReadDao(
				id(),
				bookCopyId(),
				userId(),
				rentedAt(),
				dueDate(),
				state
		);
	}
}
