package e.library.on.containers.rentals.exceptions;

public class RentalForBookDoesntExistsException  extends RuntimeException {
    public RentalForBookDoesntExistsException(int bookInstanceId) {
        super("Rental for given book (%s) doesn't exist!".formatted(bookInstanceId));
    }
}
