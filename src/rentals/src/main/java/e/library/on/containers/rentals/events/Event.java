package e.library.on.containers.rentals.events;

import lombok.Getter;

import java.time.ZonedDateTime;
import java.util.UUID;

@Getter
public abstract class Event {

    protected final UUID id;
    protected final ZonedDateTime createdAt;

    protected Event(UUID id, ZonedDateTime createdAt) {
        this.id = id;
        this.createdAt = createdAt;
    }
}

