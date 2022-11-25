package e.library.on.containers.rentals.events

import java.util.UUID

object Event {
    data class BookExtended(val rentalId: UUID)

    data class BookRented(
        val rentalId: UUID,
        val isbn: String
    )

    data class BookReturned(val bookReturned: UUID)

    data class BookAvailableForRent(val isbn: String)

    data class BookUnavailableForRent(val isbn: String)

    data class BookCopyAdded(
        val isbn: String,
        val bookCopyId: UUID
    )

    data class BookCopyRemoved(val bookCopyId:UUID)
}
