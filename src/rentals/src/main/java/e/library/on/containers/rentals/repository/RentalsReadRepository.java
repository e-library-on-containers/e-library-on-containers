package e.library.on.containers.rentals.repository;

import e.library.on.containers.rentals.repository.entity.RentalEntity;
import org.jetbrains.annotations.NotNull;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.Optional;

public interface RentalsReadRepository extends PagingAndSortingRepository<RentalEntity, String> {

    @NotNull
    List<RentalEntity> findAll();

    List<RentalEntity> findAllByUserId(String userId);

    Optional<RentalEntity> findByBookInstanceId(int bookInstanceId);
}
