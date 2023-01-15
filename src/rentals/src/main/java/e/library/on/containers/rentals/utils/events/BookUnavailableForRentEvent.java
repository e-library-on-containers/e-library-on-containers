package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookUnavailableForRentEvent extends Event {
	private final UUID bookId;

	@JsonCreator
	public BookUnavailableForRentEvent(
			UUID id,
			UUID bookId,
			ZonedDateTime createdAt
	) {
		super(id, createdAt);
		this.bookId = bookId;
	}
}
