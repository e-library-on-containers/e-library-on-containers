package e.library.on.containers.rentals.exceptions;

import java.util.UUID;

public class RentDoesNotExistException extends RuntimeException {
    public RentDoesNotExistException(UUID id) {
        super("Rent with given ID (%s) doesn't exist!".formatted(id));
    }
}
