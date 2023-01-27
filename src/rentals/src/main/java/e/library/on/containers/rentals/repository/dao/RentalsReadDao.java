package e.library.on.containers.rentals.repository.dao;

import e.library.on.containers.rentals.events.BookRentedEvent;

import java.time.ZonedDateTime;
import java.util.UUID;

public record RentalsReadDao(
		UUID id,
		int bookCopyId,
		UUID userId,
		ZonedDateTime rentedAt,
		ZonedDateTime dueDate,
		boolean wasExtended
) {
	public static RentalsReadDao from(BookRentedEvent event) {
		return new RentalsReadDao(
				event.getRentalId(),
				event.getBookInstanceId(),
				event.getUserId(),
				event.getCreatedAt(),
				event.getCreatedAt().plusDays(event.getForHowManyDays()),
				false
		);
	}

	public RentalsReadDao withExtendedRent(int days) {
		return new RentalsReadDao(
				id(),
				bookCopyId(),
				userId(),
				rentedAt(),
				dueDate().plusDays(days),
				true
		);
	}
}
