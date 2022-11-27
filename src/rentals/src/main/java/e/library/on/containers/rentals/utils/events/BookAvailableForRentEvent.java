package e.library.on.containers.rentals.utils.events;

import java.time.ZonedDateTime;
import java.util.UUID;

public record BookAvailableForRentEvent(
		UUID id,
		UUID userId,
		ZonedDateTime createdAt,
		String isbn) implements Event {
	@Override
	public UUID getId() {
		return id;
	}
}
