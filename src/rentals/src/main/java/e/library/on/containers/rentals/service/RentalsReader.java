package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.common.RentalEntityMapper;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import e.library.on.containers.rentals.repository.entity.RentalState;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

@Service
@RequiredArgsConstructor
public class RentalsReader {
	private final RentalEntityMapper mapper = new RentalEntityMapper();
	private final RentalsReadRepository readRepository;

	public List<RentalsReadDao> readAllRentals() {
		return readRepository.findAll().stream().map(mapper::entityToDao).toList();
	}

	public List<RentalsReadDao> readAllRentals(UUID userId) {
		return readRepository.findAllByUserIdAndRentalStateIsNot(userId, RentalState.RETURNED).stream().map(mapper::entityToDao).toList();
	}
}
