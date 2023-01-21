package e.library.on.containers.rentals.repository.dao;

import e.library.on.containers.rentals.utils.events.BookRentedEvent;

import java.time.ZonedDateTime;
import java.util.UUID;

public record RentalsReadDao(
		UUID id,
		UUID bookCopyId,
		UUID userId,
		ZonedDateTime rentedAt,
		ZonedDateTime dueDate,
		boolean wasExtended
) {
	public static RentalsReadDao from(BookRentedEvent event) {
		return new RentalsReadDao(
				event.getRentalId(),
				event.getUserId(),
				event.getBookId(),
				event.getCreatedAt(),
				event.getCreatedAt().plusDays(event.getForHowManyDays()),
				false
		);
	}

	public RentalsReadDao withExtendedRent(int days) {
		return new RentalsReadDao(
				id(),
				userId(),
				bookCopyId(),
				rentedAt(),
				dueDate().plusDays(days),
				true
		);
	}
}
