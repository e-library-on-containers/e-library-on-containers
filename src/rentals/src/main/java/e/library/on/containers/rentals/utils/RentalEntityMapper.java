package e.library.on.containers.rentals.utils;

import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import e.library.on.containers.rentals.repository.entity.RentalEntity;
import org.springframework.stereotype.Component;

import java.time.ZonedDateTime;
import java.util.UUID;

@Component
public class RentalEntityMapper {
    public RentalEntity daoToEntity(RentalsReadDao dao, ZonedDateTime lastEdit) {
        if (dao == null) {
            return null;
        }

        return new RentalEntity(
                dao.id().toString(),
                dao.bookCopyId().toString(),
                dao.userId().toString(),
                dao.rentedAt(),
                dao.dueDate(),
                dao.wasExtended(),
                lastEdit);
    }

    public RentalEntity daoToEntity(RentalsReadDao dao) {
        return daoToEntity(dao, ZonedDateTime.now());
    }

    public RentalsReadDao entityToDao(RentalEntity entity) {
        if (entity == null) {
            return null;
        }

        return new RentalsReadDao(
                UUID.fromString(entity.getId()),
                UUID.fromString(entity.getBookCopyId()),
                UUID.fromString(entity.getUserId()),
                entity.getRentedAt(),
                entity.getDueDate(),
                entity.isExtended());
    }
}
