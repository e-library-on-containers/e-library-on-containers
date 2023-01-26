package e.library.on.containers.rentals.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookReturnedEvent extends Event {
	private final UUID rentalId;
	private final UUID userId;
	private final int bookInstanceId;

	@JsonCreator
	public BookReturnedEvent(
			UUID id,
			ZonedDateTime createdAt,
			UUID userId,
			UUID rentalId,
			int bookInstanceId
	) {
		super(id, createdAt);
		this.userId = userId;
		this.rentalId = rentalId;
		this.bookInstanceId = bookInstanceId;
	}

	public BookReturnedEvent(UUID rentalId, UUID userId, int bookInstanceId) {
		this(UUID.randomUUID(), ZonedDateTime.now(), userId, rentalId, bookInstanceId);
	}
}
