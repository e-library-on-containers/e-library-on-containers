package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookRentedEvent extends Event {
	private final UUID rentalId;
	private final UUID userId;
	private final UUID bookId;
	private final int forHowManyDays;

	@JsonCreator
	public BookRentedEvent(
			UUID id,
			ZonedDateTime createdAt,
			UUID userId,
			UUID rentalId,
			UUID bookId,
			int forHowManyDays
	) {
		super(id, createdAt);
		this.rentalId = rentalId;
		this.userId = userId;
		this.bookId = bookId;
		this.forHowManyDays = forHowManyDays;
	}

	@Override
	public UUID getId() {
		return id;
	}

	public BookRentedEvent(
			UUID userId, UUID bookId,
			UUID rentalId, int forHowManyDays) {
		this(UUID.randomUUID(), ZonedDateTime.now(), userId, rentalId, bookId, forHowManyDays);
	}
}
