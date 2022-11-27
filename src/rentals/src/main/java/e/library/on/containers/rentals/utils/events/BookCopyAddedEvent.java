package e.library.on.containers.rentals.utils.events;

import java.time.ZonedDateTime;
import java.util.UUID;

public record BookCopyAddedEvent(
		UUID id,
		UUID userId,
		ZonedDateTime createdAt,
		String isbn,
		UUID bookCopyId
) implements Event {
	@Override
	public UUID getId() {
		return id;
	}
}
