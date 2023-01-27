package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.common.EventEntityMapper;
import e.library.on.containers.rentals.configuration.RabbitmqProperties;
import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;
import e.library.on.containers.rentals.events.Event;
import e.library.on.containers.rentals.repository.RentalsEventRepository;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Slf4j
@RequiredArgsConstructor
@Service
public class RentalsService {
    private static final int DEFAULT_RENTAL_LENGTH = 30;
    private final RabbitmqProperties config;
    private final RabbitTemplate rabbitTemplate;
    private final RentalsEventRepository eventRepository;
    private final RentalsReadRepository readRepository;
    private final EventEntityMapper eventEntityMapper;

    public UUID rentBook(UUID userId, int bookInstanceId) {
        var newRentalId = UUID.randomUUID();
        var currentRent = readRepository.findByBookInstanceId(bookInstanceId);
        if (currentRent.isPresent()) {
            throw new IllegalStateException();
        }
        var newRental = new BookRentedEvent(userId, bookInstanceId, newRentalId, DEFAULT_RENTAL_LENGTH);

        log.info("Renting book {}...", bookInstanceId);
        eventRepository.save(eventEntityMapper.eventToEntity(newRental));
        sendMessage("rental.rent", newRental);
        return newRental.getRentalId();
    }

    public void returnBook(UUID rentId, UUID userId)  {
        var rental = readRepository.findById(rentId.toString()).orElseThrow();
        var returnBook = new BookReturnedEvent(rentId, userId, rental.getBookInstanceId());

        log.info("Returning book on rent with id {}...", rentId);
        eventRepository.save(eventEntityMapper.eventToEntity(returnBook));
        sendMessage("rental.return", returnBook);
    }

    public void extendRent(UUID userId, UUID rentId, int days) {
        var extendRent = new BookExtendedEvent(userId, rentId, days);

        log.info("Extending rental {} by {} days...", rentId, days);
        eventRepository.save(eventEntityMapper.eventToEntity(extendRent));
        sendMessage("rental.extend", extendRent);
    }

    private void sendMessage(String routingKey, Event message) {
        log.debug("Sending message {}", message.toString());
        rabbitTemplate.convertAndSend(config.topicExchange().name(), routingKey, message);
    }
}
