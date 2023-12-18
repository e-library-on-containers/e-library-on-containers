package e.library.on.containers.rentals.repository;

import e.library.on.containers.rentals.repository.entity.BorrowEntity;
import org.jetbrains.annotations.NotNull;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.List;
import java.util.UUID;

public interface BorrowReadRepository extends PagingAndSortingRepository<BorrowEntity, UUID> {
    @NotNull
    List<BorrowEntity> findAll();
    List<BorrowEntity> findAllByPreviousOwnerAndAcceptedIsFalse(UUID previousOwner);
}
