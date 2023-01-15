package e.library.on.containers.rentals.utils.events;

import java.util.List;

public class MasstransitEventWrapper<T extends Event> {
    private final List<String> messageType;
    private final T message;

    public MasstransitEventWrapper(T message) {
        this.message = message;
        this.messageType = List.of(message.getClass().getSimpleName());
    }

    public T unwrap() {
        return message;
    }

    public static <S extends Event> MasstransitEventWrapper<S> wrap(S event) {
        return new MasstransitEventWrapper<>(event);
    }
}
