package e.library.on.containers.rentals.web;

import e.library.on.containers.rentals.repository.dao.BorrowReadDao;
import e.library.on.containers.rentals.repository.dao.RentalsReadDao;
import e.library.on.containers.rentals.service.BorrowReader;
import e.library.on.containers.rentals.service.RentalsReader;
import e.library.on.containers.rentals.service.RentalsService;
import e.library.on.containers.rentals.web.entity.AcceptBorrowRequest;
import e.library.on.containers.rentals.web.entity.CreateBorrowRequest;
import e.library.on.containers.rentals.web.entity.ExtendBookRentRequest;
import e.library.on.containers.rentals.web.entity.RentBookRequest;
import e.library.on.containers.rentals.web.entity.RentBookResponse;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
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

@RequiredArgsConstructor
@RestController
@RequestMapping(value = "api/rents")
class RentalsController {
    private final RentalsService service;
    private final RentalsReader rentalsReader;
    private final BorrowReader borrowReader;

    @ResponseStatus(code = HttpStatus.CREATED)
    @PostMapping(
            consumes = MediaType.APPLICATION_JSON_VALUE,
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    RentBookResponse rentBook(
            @RequestHeader(name = "X-User-Id") UUID userId,
            @RequestBody RentBookRequest rentBookRequest
    ) {
        final var rentId = service.rentBook(userId, rentBookRequest.bookInstanceId());
        return new RentBookResponse(rentId);
    }

    @ResponseStatus(code = HttpStatus.OK)
    @PostMapping(
            value = "/{rentId}/return",
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    void returnBook(
            @RequestHeader(name = "X-User-Id") UUID userId,
            @PathVariable UUID rentId
    ) {
        service.returnBook(userId, rentId);
    }

    @ResponseStatus(code = HttpStatus.OK)
    @PostMapping(
            value = "/{rentId}/approve-return",
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    void approveReturnedBook(
            @PathVariable UUID rentId
    ) {
        service.approveReturn(rentId);
    }

    @ResponseStatus(code = HttpStatus.OK)
    @GetMapping(produces = MediaType.APPLICATION_JSON_VALUE)
    List<RentalsReadDao> allRentals(
            @RequestHeader(name = "X-User-Id", required = false) UUID userId
    ) {
        return userId == null ? rentalsReader.readAllRentals() : rentalsReader.readAllRentals(userId);
    }

    @ResponseStatus(code = HttpStatus.ACCEPTED)
    @PostMapping(
            value = "/{rentId}/extend",
            produces = MediaType.APPLICATION_JSON_VALUE,
            consumes = MediaType.APPLICATION_JSON_VALUE
    )
    void extendBookRental(
            @RequestHeader(name = "X-User-Id", required = false) UUID userId,
            @PathVariable UUID rentId,
            @RequestBody ExtendBookRentRequest extendBookRentRequest) {
        service.extendRent(userId, rentId, extendBookRentRequest.days());
    }

    @ResponseStatus(code = HttpStatus.ACCEPTED)
    @PostMapping(
            value = "/borrow",
            produces = MediaType.APPLICATION_JSON_VALUE,
            consumes = MediaType.APPLICATION_JSON_VALUE
    )
    void borrow(
            @RequestHeader(name = "X-User-Id", required = false) UUID userId,
            @RequestBody CreateBorrowRequest createBorrowRequest) {
        service.borrow(userId, createBorrowRequest);
    }

    @ResponseStatus(code = HttpStatus.ACCEPTED)
    @PostMapping(
            value = "/accept-borrow",
            produces = MediaType.APPLICATION_JSON_VALUE,
            consumes = MediaType.APPLICATION_JSON_VALUE
    )
    void acceptBorrow(
            @RequestHeader(name = "X-User-Id", required = false) UUID userId,
            @RequestBody AcceptBorrowRequest acceptBorrowRequest) {
        service.acceptBorrow(userId, acceptBorrowRequest);
    }

    @ResponseStatus(code = HttpStatus.OK)
    @GetMapping(
            value = "/borrows",
            produces = MediaType.APPLICATION_JSON_VALUE
    )
    List<BorrowReadDao> getAllBorrows(@RequestHeader(name = "X-User-Id", required = false) UUID userId) {
        return userId == null ? borrowReader.getAllBorrows() : borrowReader.getAllBorrowsByPreviousOwner(userId);
    }
}
