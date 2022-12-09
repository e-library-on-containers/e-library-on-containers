package e.library.on.containers.rentals.utils.events;

import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public abstract sealed class Event permits BookRentedEvent, BookReturnedEvent, BookAvailableForRentEvent,
        BookCopyAddedEvent, BookCopyRemovedEvent, BookExtendedEvent, BookUnavailableForRentEvent {
    final UUID id;
    final UUID userId;
    final ZonedDateTime createdAt;

    protected Event(UUID id, UUID userId, ZonedDateTime createdAt) {
        this.id = id;
        this.userId = userId;
        this.createdAt = createdAt;
    }
}

