package e.library.on.containers.rentals.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;
import lombok.ToString;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
@ToString
public final class BookRentedEvent extends Event {
	private final UUID rentalId;
	private final UUID userId;
	private final int bookInstanceId;
	private final int forHowManyDays;

	@JsonCreator
	public BookRentedEvent(
			UUID id,
			ZonedDateTime createdAt,
			UUID userId,
			UUID rentalId,
			int bookInstanceId,
			int forHowManyDays
	) {
		super(id, createdAt);
		this.rentalId = rentalId;
		this.userId = userId;
		this.bookInstanceId = bookInstanceId;
		this.forHowManyDays = forHowManyDays;
	}

	@Override
	public UUID getId() {
		return id;
	}

	public BookRentedEvent(UUID userId, int bookInstanceId, UUID rentalId, int forHowManyDays) {
		this(UUID.randomUUID(), ZonedDateTime.now(), userId, rentalId, bookInstanceId, forHowManyDays);
	}
}
