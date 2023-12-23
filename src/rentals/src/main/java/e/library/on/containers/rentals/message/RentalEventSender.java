package e.library.on.containers.rentals.message;

import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnApprovedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;

public interface RentalEventSender {
    void send(BookRentedEvent event);
    void send(BookReturnedEvent event);
    void send(BookExtendedEvent event);
    void send(BookReturnApprovedEvent event);
}
