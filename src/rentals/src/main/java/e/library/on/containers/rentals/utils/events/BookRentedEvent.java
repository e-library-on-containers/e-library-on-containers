package e.library.on.containers.rentals.utils.events;

import java.time.ZonedDateTime;
import java.util.UUID;

public record BookRentedEvent(
		UUID id,
		UUID userId,
		ZonedDateTime createdAt,
		UUID rentalId,
		String isbn,
		int forHowManyDays
) implements Event {
	@Override
	public UUID getId() {
		return id;
	}

	public BookRentedEvent(UUID userId, UUID rentalId, String isbn, int forHowManyDays) {
		this(UUID.randomUUID(), userId, ZonedDateTime.now(), rentalId, isbn, forHowManyDays);
	}
}
