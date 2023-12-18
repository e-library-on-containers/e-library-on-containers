package e.library.on.containers.rentals.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;
import lombok.ToString;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
@ToString
public class BorrowRequestEvent extends Event {
    private final int bookInstanceId;
    private final UUID originalOwner;
    private final UUID newOwner;

    @JsonCreator
    public BorrowRequestEvent(
            UUID id,
            ZonedDateTime createdAt,
            int bookInstanceId,
            UUID originalOwner,
            UUID newOwner) {
        super(id, createdAt);
        this.bookInstanceId = bookInstanceId;
        this.originalOwner = originalOwner;
        this.newOwner = newOwner;
    }

    public BorrowRequestEvent(int bookInstanceId, UUID originalOwner, UUID newOwner) {
        this(UUID.randomUUID(), ZonedDateTime.now(), bookInstanceId, originalOwner, newOwner);
    }
}
