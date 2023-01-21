package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import e.library.on.containers.rentals.utils.RentalEntityMapper;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.UUID;

@Service
@RequiredArgsConstructor
public class RentalsReader {
	private final RentalEntityMapper mapper;
	private final RentalsReadRepository readRepository;

	public List<RentalsReadDao> readAllRentals() {
		return readRepository.findAll().stream().map(mapper::entityToDao).toList();
	}

	Optional<RentalsReadDao> readRental(UUID rentalId) {
		return readRepository.findById(rentalId.toString()).map(mapper::entityToDao);
	}
}
