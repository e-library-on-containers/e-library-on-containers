package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.common.EventEntityMapper;
import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnApprovedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;
import e.library.on.containers.rentals.exceptions.RentDoesNotExistException;
import e.library.on.containers.rentals.exceptions.RentalForBookAlreadyExistsException;
import e.library.on.containers.rentals.message.RentalEventSender;
import e.library.on.containers.rentals.repository.RentalsEventRepository;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Slf4j
@RequiredArgsConstructor
@Service
public class RentalsService {
    private static final int DEFAULT_RENTAL_LENGTH = 30;

    private final RentalsEventRepository eventRepository;
    private final RentalsReadRepository readRepository;
    private final RentalEventSender eventSender;

    private final EventEntityMapper eventEntityMapper = new EventEntityMapper();

    public UUID rentBook(UUID userId, int bookInstanceId) {
        readRepository.findByBookInstanceId(bookInstanceId).ifPresent(
                $ -> {
                    throw new RentalForBookAlreadyExistsException(bookInstanceId);
                }
        );

        final var newRentalId = UUID.randomUUID();
        final var rentEvent = new BookRentedEvent(userId, bookInstanceId, newRentalId, DEFAULT_RENTAL_LENGTH);

        log.info("Renting book {}...", bookInstanceId);
        eventRepository.save(eventEntityMapper.eventToEntity(rentEvent));
        eventSender.send(rentEvent);
        return rentEvent.getRentalId();
    }

    public void returnBook(UUID userId, UUID rentId)  {
        final var rental = readRepository.findById(rentId)
                .orElseThrow(() -> new RentDoesNotExistException(rentId));
        final var returnEvent = new BookReturnedEvent(rentId, userId, rental.getBookInstanceId());

        log.info("Returning book on rent with id {}...", rentId);
        eventRepository.save(eventEntityMapper.eventToEntity(returnEvent));
        eventSender.send(returnEvent);
    }

    public void approveReturn(UUID rentId) {
        final var rental = readRepository.findById(rentId)
                .orElseThrow(() -> new RentDoesNotExistException(rentId));
        final var approvedReturnEvent = new BookReturnApprovedEvent(rental.getId());

        log.info("Approved return of rent with id: {}", rental.getId());
        eventRepository.save(eventEntityMapper.eventToEntity(approvedReturnEvent));
        eventSender.send(approvedReturnEvent);
    }

    public void extendRent(UUID userId, UUID rentId, int days) {
        if (readRepository.findById(rentId).isEmpty()) {
            throw new RentDoesNotExistException(rentId);
        }
        final var extendedEvent = new BookExtendedEvent(userId, rentId, days);

        log.info("Extending rental {} by {} days...", rentId, days);
        eventRepository.save(eventEntityMapper.eventToEntity(extendedEvent));
        eventSender.send(extendedEvent);
    }
}
