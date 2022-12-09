package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookCopyAddedEvent extends Event {
	private final String isbn;
	private final UUID bookCopyId;

	@JsonCreator
	public BookCopyAddedEvent(
			UUID id,
			UUID userId,
			ZonedDateTime createdAt,
			String isbn,
			UUID bookCopyId
	) {
		super(id, userId, createdAt);
		this.isbn = isbn;
		this.bookCopyId = bookCopyId;
	}
}
