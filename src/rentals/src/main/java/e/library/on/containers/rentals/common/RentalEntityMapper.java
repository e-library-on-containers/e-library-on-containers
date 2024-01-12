package e.library.on.containers.rentals.common;

import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import e.library.on.containers.rentals.repository.entity.RentalEntity;

import java.time.ZonedDateTime;

public class RentalEntityMapper {
    public RentalEntity daoToEntity(RentalsReadDao dao, ZonedDateTime lastEdit) {
        return dao == null ? null :
                new RentalEntity(
                        dao.id(),
                        dao.bookCopyId(),
                        dao.userId(),
                        dao.rentedAt(),
                        dao.dueDate(),
                        dao.rentalState(),
                        lastEdit
                );
    }

    public RentalEntity daoToEntity(RentalsReadDao dao) {
        return daoToEntity(dao, ZonedDateTime.now());
    }

    public RentalsReadDao entityToDao(RentalEntity entity) {
        if (entity == null) {
            return null;
        }

        return new RentalsReadDao(
                entity.getId(),
                entity.getBookInstanceId(),
                entity.getUserId(),
                entity.getRentedAt(),
                entity.getDueDate(),
                entity.getRentalState()
        );
    }
}
