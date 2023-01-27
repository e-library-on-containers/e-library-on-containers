package e.library.on.containers.rentals.repository.dao;

import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;

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
    public static EventDao from(BookRentedEvent event) {
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

    public static EventDao from(BookReturnedEvent event) {
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

    public static EventDao from(BookExtendedEvent event) {
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
}
