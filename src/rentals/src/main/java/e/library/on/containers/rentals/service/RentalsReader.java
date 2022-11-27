package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.utils.RentalsReadDao;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.UUID;

@Service
public class RentalsReader {
	private final RentalsReadRepository readRepository;

	public RentalsReader(RentalsReadRepository readRepository) {
		this.readRepository = readRepository;
	}

	public List<RentalsReadDao> readAllRentals() {
		return readRepository.getAllRentals();
	}

	Optional<RentalsReadDao> readRental(UUID rentalId) {
		return readRepository.getRentalById(rentalId);
	}
}
