package e.library.on.containers.rentals.utils.events;

import java.time.ZonedDateTime;
import java.util.UUID;

public record BookCopyRemovedEvent(
		UUID id,
		UUID userId,
		ZonedDateTime createdAt,
		UUID bookCopyId
) implements Event {
	@Override
	public UUID getId() {
		return id;
	}
}
