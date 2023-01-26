package e.library.on.containers.rentals.web;

import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import e.library.on.containers.rentals.service.RentalsReader;
import e.library.on.containers.rentals.service.RentalsService;
import e.library.on.containers.rentals.web.entity.ExtendBookRentRequest;
import e.library.on.containers.rentals.web.entity.RentBookRequest;
import e.library.on.containers.rentals.web.entity.RentBookResponse;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseStatus;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping(value = "api/rents")
class RentalsController {
    private final RentalsService service;
    private final RentalsReader reader;

    RentalsController(RentalsService service, RentalsReader reader) {
        this.service = service;
        this.reader = reader;
    }

    @ResponseStatus(code = HttpStatus.CREATED)
    @PostMapping(
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    RentBookResponse rentBook(
            @RequestHeader(name = "X-User-Id") UUID userId,
            @RequestBody RentBookRequest rentBookRequest
    ) {
        var rentId = service.rentBook(userId, rentBookRequest.bookInstanceId());
        return new RentBookResponse(rentId);
    }

    @ResponseStatus(code = HttpStatus.NO_CONTENT)
    @DeleteMapping(
            value = "/{rentId}/return",
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    void returnBook(
            @RequestHeader(name = "X-User-Id") UUID userId,
            @PathVariable UUID rentId
    ) {
        service.returnBook(rentId, userId);
    }

    @ResponseStatus(code = HttpStatus.OK)
    @GetMapping(
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    List<RentalsReadDao> allRentals(
            @RequestHeader(name = "X-User-Id", required = false) UUID userId
    ) {
        return userId == null ? reader.readAllRentals() : reader.readAllRentals(userId);
    }

    @ResponseStatus(code = HttpStatus.ACCEPTED)
    @PostMapping(
            value = "/{rentId}/extend",
            produces = MediaType.APPLICATION_JSON_VALUE,
            consumes = MediaType.APPLICATION_JSON_VALUE
    )
    void extendBookRental(@PathVariable UUID rentId, @RequestBody ExtendBookRentRequest extendBookRentRequest) {
        service.extendRent(rentId, extendBookRentRequest.days());
    }
}
