package e.library.on.containers.rentals.repository;

import e.library.on.containers.rentals.utils.events.Event;
import java.time.ZonedDateTime;
import java.util.List;

public interface RentalsEventRepository {
    void addEvent(Event event);

    List<Event> getAllEventsPast(ZonedDateTime data);
}
