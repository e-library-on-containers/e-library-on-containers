package e.library.on.containers.rentals.utils.events;

import com.fasterxml.jackson.annotation.JsonCreator;
import lombok.Getter;

import java.util.List;

@Getter
public class MasstransitEventWrapper<T extends Event> {
    private final List<String> messageType;
    private final T message;

    @JsonCreator
    public MasstransitEventWrapper(List<String> messageType, T message) {
        this.messageType = messageType;
        this.message = message;
    }

    public MasstransitEventWrapper(T message) {
        this(List.of(message.getClass().getSimpleName()), message);
    }

    public static <S extends Event> MasstransitEventWrapper<S> wrap(S event) {
        return new MasstransitEventWrapper<>(event);
    }
}
