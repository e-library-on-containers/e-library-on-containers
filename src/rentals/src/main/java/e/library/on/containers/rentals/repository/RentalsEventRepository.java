package e.library.on.containers.rentals.repository;

import e.library.on.containers.rentals.repository.entity.EventEntity;
import org.springframework.data.repository.PagingAndSortingRepository;

import java.util.UUID;

public interface RentalsEventRepository extends PagingAndSortingRepository<EventEntity, UUID> {
}
