package e.library.on.containers.rentals.repository;

import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import java.time.ZonedDateTime;
import java.util.List;
import java.util.UUID;
import java.util.Optional;

public interface RentalsReadRepository {
    List<RentalsReadDao> getAllRentals();

    Optional<RentalsReadDao> getRentalById(UUID rentId);

    void insertRental(RentalsReadDao readDao);

    void updateRental(RentalsReadDao readDao);

    boolean removeRental(UUID rentId);

    void updateLastModificationDate();

    ZonedDateTime getLastModificationDate();
}
