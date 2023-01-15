package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.utils.events.BookExtendedEvent;
import e.library.on.containers.rentals.utils.events.BookRentedEvent;
import e.library.on.containers.rentals.utils.events.BookReturnedEvent;
import e.library.on.containers.rentals.configuration.RabbitmqProperties;
import e.library.on.containers.rentals.repository.RentalsEventRepository;
import e.library.on.containers.rentals.utils.events.Event;
import e.library.on.containers.rentals.utils.events.MasstransitEventWrapper;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.stereotype.Service;
import java.util.UUID;

@Slf4j
@Service
public class RentalsService {
    private static final int DEFAULT_RENTAL_LENGTH = 30;
    private final RabbitmqProperties config;
    private final RabbitTemplate rabbitTemplate;
    private final RentalsEventRepository eventRepository;

    public RentalsService(
            RabbitmqProperties config,
            RabbitTemplate rabbitTemplate,
            RentalsEventRepository eventRepository) {
        this.config = config;
        this.rabbitTemplate = rabbitTemplate;
        this.eventRepository = eventRepository;
    }

    public UUID rentBook(UUID userId, UUID bookId) {
        var newRentalId = UUID.randomUUID();
        var newRental = new BookRentedEvent(userId, bookId, newRentalId, DEFAULT_RENTAL_LENGTH);

        log.info("Renting book {}...", bookId);
        eventRepository.addEvent(newRental);
        sendMessage("rental.rent", newRental);
        return newRental.getRentalId();
    }

    public void returnBook(UUID rentId, UUID userId)  {
        var returnBook = new BookReturnedEvent(rentId, userId);

        log.info("Returning book on rent with id {}...", rentId);
        eventRepository.addEvent(returnBook);
        sendMessage("rental.return", returnBook);
    }

    public void extendRent(UUID rentId, int days) {
        var extendRent = new BookExtendedEvent(rentId, days);

        log.info("Extending rental {} by {} days...", rentId, days);
        eventRepository.addEvent(extendRent);
        sendMessage("rental.extend", extendRent);
    }

    private void sendMessage(String routingKey, Event message) {
        log.debug("Sending message {}", message.toString());
        rabbitTemplate.convertAndSend(config.topicExchange().name(), routingKey, MasstransitEventWrapper.wrap(message));
    }
}
