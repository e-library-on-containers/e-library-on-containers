package e.library.on.containers.rentals.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;
import lombok.ToString;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
@ToString
public class ReturnAwaitingApprovalEvent extends Event {
    private final UUID rentalId;
    private final UUID userId;
    private final int bookInstanceId;

    @JsonCreator
    public ReturnAwaitingApprovalEvent(
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

    public ReturnAwaitingApprovalEvent(UUID rentalId, UUID userId, int bookInstanceId) {
        this(UUID.randomUUID(), ZonedDateTime.now(), userId, rentalId, bookInstanceId);
    }
}
