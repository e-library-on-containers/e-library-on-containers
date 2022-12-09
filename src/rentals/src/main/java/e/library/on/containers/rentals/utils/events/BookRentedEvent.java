package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public final class BookRentedEvent extends Event {
	private final UUID rentalId;
	private final String isbn;
	private final int forHowManyDays;

	@JsonCreator
	public BookRentedEvent(
			UUID id,
			UUID userId,
			ZonedDateTime createdAt,
			UUID rentalId,
			String isbn,
			int forHowManyDays
	) {
		super(id, userId, createdAt);
		this.rentalId = rentalId;
		this.isbn = isbn;
		this.forHowManyDays = forHowManyDays;
	}

	@Override
	public UUID getId() {
		return id;
	}

	public BookRentedEvent(UUID userId, UUID rentalId, String isbn, int forHowManyDays) {
		this(UUID.randomUUID(), userId, ZonedDateTime.now(), rentalId, isbn, forHowManyDays);
	}
}
