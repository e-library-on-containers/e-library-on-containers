package e.library.on.containers.rentals.common;

import e.library.on.containers.rentals.events.Event;
import e.library.on.containers.rentals.repository.dao.EventDao;
import e.library.on.containers.rentals.repository.entity.EventEntity;

public class EventEntityMapper {

    public EventEntity eventToEntity(Event event) {
        EventDao dao =  EventDao.from(event);

        return new EventEntity(
                dao.id(),
                dao.createdAt(),
                dao.rentalId(),
                dao.userId(),
                dao.bookInstanceId(),
                dao.forHowManyDays(),
                dao.days(),
                dao.eventType()
        );
    }
}
