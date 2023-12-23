package e.library.on.containers.rentals.repository.dao;

import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnApprovedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;
import e.library.on.containers.rentals.events.Event;
import e.library.on.containers.rentals.exceptions.UnsupportedEventTypeException;

import java.time.ZonedDateTime;
import java.util.UUID;

public record EventDao(
      UUID id,
      ZonedDateTime createdAt,
      UUID rentalId,
      UUID userId,
      int bookInstanceId,
      int forHowManyDays,
      int days,
      EventType eventType
){
    public static EventDao from(Event event) {
        if (event instanceof BookReturnedEvent bookReturnedEvent) {
            return EventDao.from(bookReturnedEvent);
        }
        if (event instanceof BookRentedEvent bookRentedEvent) {
            return EventDao.from(bookRentedEvent);
        }
        if (event instanceof BookExtendedEvent bookExtendedEvent) {
            return EventDao.from(bookExtendedEvent);
        }
        if (event instanceof BookReturnApprovedEvent bookReturnApprovedEvent) {
            return EventDao.from(bookReturnApprovedEvent);
        }

        throw new UnsupportedEventTypeException(event.getClass());
    }

    private static EventDao from(BookRentedEvent event) {
        return new EventDao(
                event.getId(),
                event.getCreatedAt(),
                event.getRentalId(),
                event.getUserId(),
                event.getBookInstanceId(),
                event.getForHowManyDays(),
                0,
                EventType.RENTED
        );
    }

    private static EventDao from(BookReturnedEvent event) {
        return new EventDao(
                event.getId(),
                event.getCreatedAt(),
                event.getRentalId(),
                event.getUserId(),
                event.getBookInstanceId(),
                0,
                0,
                EventType.RETURNED
        );
    }

    private static EventDao from(BookExtendedEvent event) {
        return new EventDao(
                event.getId(),
                event.getCreatedAt(),
                event.getRentalId(),
                event.getUserId(),
                0,
                0,
                event.getDays(),
                EventType.EXTENDED
        );
    }

    private static EventDao from(BookReturnApprovedEvent event) {
        return new EventDao(
                event.getId(),
                event.getCreatedAt(),
                event.getRentalId(),
                null,
                0,
                0,
                0,
                EventType.RETURN_APPROVED
        );
    }
}
