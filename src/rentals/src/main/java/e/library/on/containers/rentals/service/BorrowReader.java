package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.common.BorrowEntityMapper;
import e.library.on.containers.rentals.repository.BorrowReadRepository;
import e.library.on.containers.rentals.repository.dao.BorrowReadDao;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.UUID;

@Service
@RequiredArgsConstructor
public class BorrowReader {
    private final BorrowReadRepository readRepository;
    private final BorrowEntityMapper mapper = new BorrowEntityMapper();

    public List<BorrowReadDao> getAllBorrows() {
        return readRepository.findAll().stream().map(mapper::entityToDao).toList();
    }

    public List<BorrowReadDao> getAllBorrowsByPreviousOwner(UUID previousOwner) {
        return readRepository.findAllByPreviousOwnerAndAcceptedIsFalse(previousOwner).stream().map(mapper::entityToDao).toList();
    }
}
