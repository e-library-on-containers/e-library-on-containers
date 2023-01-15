package e.library.on.containers.rentals.repository;

import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import org.jetbrains.annotations.NotNull;
import org.springframework.stereotype.Component;

import java.time.ZonedDateTime;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.UUID;
import java.util.concurrent.ConcurrentHashMap;

@Component
class InMemoryRentalsReadRepository implements RentalsReadRepository {
	private final static Map<UUID, RentalsReadDao> inMemoryReadStore = new ConcurrentHashMap<>();
	private static ZonedDateTime lastModification = ZonedDateTime.now();

	@NotNull
	@Override
	public List<RentalsReadDao> getAllRentals() {
		return List.copyOf(inMemoryReadStore.values());
	}

	@NotNull
	@Override
	public Optional<RentalsReadDao> getRentalById(@NotNull UUID rentId) {
		return Optional.ofNullable(inMemoryReadStore.get(rentId));
	}

	@Override
	public void insertRental(@NotNull RentalsReadDao readDao) {
		inMemoryReadStore.put(readDao.id(), readDao);
	}

	@Override
	public void updateRental(RentalsReadDao readDao) {
		if (inMemoryReadStore.get(readDao.id()) == null) {
			throw new RuntimeException("Rental doesn't exists");
		}
		inMemoryReadStore.put(readDao.id(), readDao);
	}

	@Override
	public boolean removeRental(@NotNull UUID rentId) {
		if (getRentalById(rentId).isEmpty()) {
			return false;
		}
		inMemoryReadStore.remove(rentId);
		return true;
	}

	@Override
	public void updateLastModificationDate() {
		lastModification = ZonedDateTime.now();
	}

	@NotNull
	@Override
	public ZonedDateTime getLastModificationDate() {
		return lastModification;
	}
}
