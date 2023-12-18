package e.library.on.containers.rentals.common;

import e.library.on.containers.rentals.repository.dao.BorrowReadDao;
import e.library.on.containers.rentals.repository.entity.BorrowEntity;

public class BorrowEntityMapper {
    public BorrowReadDao entityToDao(BorrowEntity entity) {
        if (entity == null) {
            return null;
        }

        return new BorrowReadDao(
                entity.getId(),
                entity.getPreviousOwner(),
                entity.getNewOwner(),
                entity.getBookInstanceId(),
                entity.isAccepted(),
                entity.isAccepted() ? entity.getCreatedRental().getId() : null,
                entity.getBorrowedAt(),
                entity.getLastEditDate()
        );
    }
}
