package e.library.on.containers.rentals.utils.events;

import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public abstract sealed class Event permits BookRentedEvent, BookReturnedEvent, BookAvailableForRentEvent,
        BookCopyAddedEvent, BookCopyRemovedEvent, BookExtendedEvent, BookUnavailableForRentEvent {

    protected final UUID id;
    protected final UUID userId;
    protected final ZonedDateTime createdAt;

    protected Event(UUID id, UUID userId, ZonedDateTime createdAt) {
        this.id = id;
        this.userId = userId;
        this.createdAt = createdAt;
    }
}

