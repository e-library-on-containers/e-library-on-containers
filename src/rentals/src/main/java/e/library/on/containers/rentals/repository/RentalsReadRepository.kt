package e.library.on.containers.rentals.repository

import e.library.on.containers.rentals.repository.dao.RentalsReadDao
import java.time.ZonedDateTime
import java.util.UUID
import java.util.Optional

interface RentalsReadRepository {
    fun getAllRentals(): List<RentalsReadDao>

    fun getRentalById(rentId: UUID): Optional<RentalsReadDao>

    fun insertRental(readDao: RentalsReadDao)

    fun removeRental(rentId: UUID): Boolean

    fun updateLastModificationDate()

    fun getLastModificationDate(): ZonedDateTime
}
