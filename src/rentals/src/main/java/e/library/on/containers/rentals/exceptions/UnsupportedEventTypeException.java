package e.library.on.containers.rentals.exceptions;

import e.library.on.containers.rentals.events.Event;

public class UnsupportedEventTypeException extends RuntimeException {

    public UnsupportedEventTypeException(Class<? extends Event> clazz) {
        super("Unsupported event type given: %s".formatted(clazz.getSimpleName()));
    }
}
