package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookReturnedEvent extends Event {
	private final UUID rentalId;
	private final UUID userId;

	@JsonCreator
	public BookReturnedEvent(
			UUID id,
			ZonedDateTime createdAt,
			UUID userId,
			UUID rentalId
	) {
		super(id, createdAt);
		this.userId = userId;
		this.rentalId = rentalId;
	}

	public BookReturnedEvent(UUID rentalId, UUID userId) {
		this(UUID.randomUUID(), ZonedDateTime.now(), rentalId, userId);
	}
}
