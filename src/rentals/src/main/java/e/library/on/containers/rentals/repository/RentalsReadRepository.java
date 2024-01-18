package e.library.on.containers.rentals.repository;

import e.library.on.containers.rentals.repository.entity.RentalEntity;
import e.library.on.containers.rentals.repository.entity.RentalState;
import org.jetbrains.annotations.NotNull;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.Optional;
import java.util.UUID;

public interface RentalsReadRepository extends PagingAndSortingRepository<RentalEntity, UUID> {

    @NotNull
    List<RentalEntity> findAll();

    List<RentalEntity> findAllByUserIdAndRentalStateIsNot(UUID userId, RentalState state);

    Optional<RentalEntity> findByBookInstanceIdAndRentalStateNot(int bookInstanceId, RentalState rentalState);
    Optional<RentalEntity> findByBookInstanceIdAndRentalStateNotIn(int bookInstanceId, List<RentalState> rentalStates);
}
