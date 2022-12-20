package e.library.on.containers.rentals.repository.dao;

import e.library.on.containers.rentals.utils.events.BookRentedEvent;

import java.time.ZonedDateTime;
import java.util.UUID;

public record RentalsReadDao(
		UUID rentalId,
		String isbn,
		UUID userId,
		ZonedDateTime rentedAt,
		ZonedDateTime dueDate
) {
	public static RentalsReadDao from(BookRentedEvent event) {
		return new RentalsReadDao(
				event.getRentalId(),
				event.getIsbn(),
				event.getUserId(),
				event.getCreatedAt(),
				event.getCreatedAt().plusDays(event.getForHowManyDays())
		);
	}

	public RentalsReadDao withExtendedRent(int days) {
		return new RentalsReadDao(
				rentalId(),
				isbn(),
				userId(),
				rentedAt(),
				dueDate().plusDays(days)
		);
	}
}
