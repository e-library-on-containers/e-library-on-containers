package e.library.on.containers.rentals.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;
import lombok.ToString;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
@ToString
public class AcceptBorrowEvent extends Event{
    UUID borrowId;

    @JsonCreator
    public AcceptBorrowEvent(UUID id, ZonedDateTime createdAt, UUID borrowId) {
        super(id, createdAt);
        this.borrowId = borrowId;
    }

    public AcceptBorrowEvent(UUID borrowId) {
        this(UUID.randomUUID(), ZonedDateTime.now(), borrowId);
    }
}
