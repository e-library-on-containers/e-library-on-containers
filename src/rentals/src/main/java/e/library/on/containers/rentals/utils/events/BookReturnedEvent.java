package e.library.on.containers.rentals.utils.events;

import java.time.ZonedDateTime;
import java.util.UUID;

public record BookReturnedEvent(
		UUID id,
		UUID userId,
		ZonedDateTime createdAt,
		UUID rentalId
) implements Event {
	@Override
	public UUID getId() {
		return id;
	}

	public BookReturnedEvent(UUID rentalId) {
		this(UUID.randomUUID(), null, ZonedDateTime.now(), rentalId);
	}
}
