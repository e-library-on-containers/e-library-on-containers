package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookCopyRemovedEvent extends Event {
	private final UUID bookCopyId;

	@JsonCreator
	public BookCopyRemovedEvent(
			UUID id,
			UUID userId,
			ZonedDateTime createdAt,
			UUID bookCopyId
	) {
		super(id, userId, createdAt);
		this.bookCopyId = bookCopyId;
	}
}
