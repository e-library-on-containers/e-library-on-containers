package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.common.RentalEntityMapper;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class RentalsReader {
	private final RentalEntityMapper mapper;
	private final RentalsReadRepository readRepository;

	public List<RentalsReadDao> readAllRentals() {
		return readRepository.findAll().stream().map(mapper::entityToDao).toList();
	}
}
