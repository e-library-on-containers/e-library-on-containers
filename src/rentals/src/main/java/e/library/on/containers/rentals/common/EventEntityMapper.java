package e.library.on.containers.rentals.common;

import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;
import e.library.on.containers.rentals.events.Event;
import e.library.on.containers.rentals.repository.dao.EventDao;
import e.library.on.containers.rentals.repository.dao.EventType;
import e.library.on.containers.rentals.repository.entity.EventEntity;
import org.springframework.stereotype.Component;

import java.util.UUID;

@Component
public class EventEntityMapper {
    public Event entityToEvent(EventEntity entity) {
        return switch (EventType.valueOf(entity.getEventType())) {
            case RENTED ->  new BookRentedEvent(
                    UUID.fromString(entity.getId()),
                    entity.getCreatedAt(),
                    UUID.fromString(entity.getUserId()),
                    UUID.fromString(entity.getRentalId()),
                    entity.getBookInstanceId(),
                    entity.getForHowManyDays()
            );
            case RETURNED -> new BookReturnedEvent(
                    UUID.fromString(entity.getId()),
                    entity.getCreatedAt(),
                    UUID.fromString(entity.getUserId()),
                    UUID.fromString(entity.getRentalId()),
                    entity.getBookInstanceId()
            );
            case EXTENDED -> new BookExtendedEvent(
                    UUID.fromString(entity.getId()),
                    entity.getCreatedAt(),
                    UUID.fromString(entity.getUserId()),
                    UUID.fromString(entity.getRentalId()),
                    entity.getDays()
            );
        };
    }

    public EventEntity eventToEntity(Event event) {
        EventDao dao;
        if(event instanceof BookRentedEvent e) {
            dao = EventDao.from(e);
        } else if (event instanceof BookReturnedEvent e) {
            dao = EventDao.from(e);
        } else {
            dao = EventDao.from((BookExtendedEvent) event);
        }

        return new EventEntity(
                dao.id().toString(),
                dao.createdAt(),
                dao.rentalId().toString(),
                dao.userId().toString(),
                dao.bookInstanceId(),
                dao.forHowManyDays(),
                dao.days(),
                dao.eventType().toString()
        );
    }
}
