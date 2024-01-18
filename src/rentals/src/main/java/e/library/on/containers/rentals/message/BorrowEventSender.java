package e.library.on.containers.rentals.message;

import e.library.on.containers.rentals.events.AcceptBorrowEvent;
import e.library.on.containers.rentals.events.BorrowRequestEvent;

public interface BorrowEventSender {
    void send(BorrowRequestEvent event);
    void send(AcceptBorrowEvent event);
}
