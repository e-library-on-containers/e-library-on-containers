package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookAvailableForRentEvent extends Event {
	private final String isbn;
	private final UUID bookId;

	@JsonCreator
	public BookAvailableForRentEvent(
			UUID id,
			ZonedDateTime createdAt,
			UUID bookId,
			String isbn) {
		super(id, createdAt);
		this.isbn = isbn;
		this.bookId = bookId;
	}
}
