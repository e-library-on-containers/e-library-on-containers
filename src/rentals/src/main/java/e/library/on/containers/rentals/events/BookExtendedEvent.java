package e.library.on.containers.rentals.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;
import lombok.ToString;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
@ToString
public final class BookExtendedEvent extends Event {
	private final UUID userId;
	private final UUID rentalId;
	private final int days;

	@JsonCreator
	public BookExtendedEvent(
			UUID id,
			ZonedDateTime createdAt,
			UUID userId,
			UUID rentalId,
			int days
	) {
		super(id, createdAt);
		this.userId = userId;
		this.rentalId = rentalId;
		this.days = days;
	}

	public BookExtendedEvent(UUID userId, UUID rentalId, int days) {
		this(UUID.randomUUID(), ZonedDateTime.now(), userId, rentalId, days);
	}
}
