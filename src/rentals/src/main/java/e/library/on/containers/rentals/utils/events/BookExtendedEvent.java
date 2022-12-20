package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookExtendedEvent extends Event {
	private final UUID rentalId;
	private final int days;

	@JsonCreator
	public BookExtendedEvent(
			UUID id,
			UUID userId,
			ZonedDateTime createdAt,
			UUID rentalId,
			int days
	) {
		super(id, userId, createdAt);
		this.rentalId = rentalId;
		this.days = days;
	}

	public BookExtendedEvent(UUID rentalId, int days) {
		this(UUID.randomUUID(), null, ZonedDateTime.now(), rentalId, days);
	}
}
