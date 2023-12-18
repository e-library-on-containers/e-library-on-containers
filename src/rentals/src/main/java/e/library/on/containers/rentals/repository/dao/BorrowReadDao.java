package e.library.on.containers.rentals.repository.dao;

import java.time.ZonedDateTime;
import java.util.UUID;

public record BorrowReadDao(
        UUID id,
        UUID previousOwner,
        UUID newOwner,
        int bookInstanceId,
        boolean isAccepted,
        UUID rentalId,
        ZonedDateTime borrowedAt,
        ZonedDateTime lastEditDate
) {
}
