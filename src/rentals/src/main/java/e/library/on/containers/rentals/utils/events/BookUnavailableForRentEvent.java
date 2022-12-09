package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookUnavailableForRentEvent extends Event {
	private final String isbn;

	@JsonCreator
	public BookUnavailableForRentEvent(
			UUID id,
			UUID userId,
			ZonedDateTime createdAt,
			String isbn) {
		super(id, userId, createdAt);
		this.isbn = isbn;
	}
}
