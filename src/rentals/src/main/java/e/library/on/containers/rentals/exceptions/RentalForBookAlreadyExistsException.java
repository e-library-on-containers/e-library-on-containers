package e.library.on.containers.rentals.exceptions;

public class RentalForBookAlreadyExistsException extends RuntimeException {
    public RentalForBookAlreadyExistsException(int bookInstanceId) {
        super("Rental for given book (%s) already exists!".formatted(bookInstanceId));
    }
}
