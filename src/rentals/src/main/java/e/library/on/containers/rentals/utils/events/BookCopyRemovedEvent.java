package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookCopyRemovedEvent extends Event {
	private final UUID bookId;

	@JsonCreator
	public BookCopyRemovedEvent(
			UUID id,
			UUID bookId,
			ZonedDateTime createdAt
	) {
		super(id, createdAt);
		this.bookId = bookId;
	}
}
