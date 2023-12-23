package e.library.on.containers.rentals.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public class BookReturnApprovedEvent extends Event{
    private final UUID rentalId;

    @JsonCreator
    public BookReturnApprovedEvent(
            UUID id,
            ZonedDateTime createdAt,
            UUID rentalId
    ) {
        super(id, createdAt);
        this.rentalId = rentalId;
    }

    public BookReturnApprovedEvent(UUID rentalId) {
        this(UUID.randomUUID(), ZonedDateTime.now(), rentalId);
    }
}
