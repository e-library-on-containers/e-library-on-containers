package e.library.on.containers.rentals

import e.library.on.containers.rentals.common.RentBookRequest
import e.library.on.containers.rentals.common.RentBookResponse
import org.springframework.http.HttpStatus
import org.springframework.http.MediaType
import org.springframework.web.bind.annotation.DeleteMapping
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.PathVariable
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.ResponseStatus
import org.springframework.web.bind.annotation.RestController
import java.util.UUID

@RestController
@RequestMapping(value = ["api/rents"])
internal class RentalsController(private val service: RentalService) {

    @ResponseStatus(code = HttpStatus.CREATED)
    @PostMapping(
        consumes = [MediaType.APPLICATION_JSON_VALUE],
        produces = [MediaType.APPLICATION_JSON_VALUE]
    )
    fun rentBook(@RequestBody rentBookRequest: RentBookRequest): RentBookResponse {
        return RentBookResponse(rentId = service.rentBook(rentBookRequest.isbn))
    }

    @ResponseStatus(code = HttpStatus.NO_CONTENT)
    @DeleteMapping(
        value = ["/{rentId}/return"],
        produces = [MediaType.APPLICATION_JSON_VALUE]
    )
    fun returnBook(@PathVariable rentId: UUID) {
        service.returnBook(rentId)
    }

    @ResponseStatus(code = HttpStatus.OK)
    @GetMapping(
        produces = [MediaType.APPLICATION_JSON_VALUE]
    )
    fun allRentals(): String {
        throw RuntimeException("Getting all rentals is not implemented yet!")
    }
}
