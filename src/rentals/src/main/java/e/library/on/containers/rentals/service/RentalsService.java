package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.utils.events.BookRentedEvent;
import e.library.on.containers.rentals.utils.events.BookReturnedEvent;
import e.library.on.containers.rentals.configuration.RabbitmqProperties;
import e.library.on.containers.rentals.repository.RentalsEventRepository;
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

    public UUID rentBook(UUID userId, String isbn) {
        var newRentalId = UUID.randomUUID();
        var newRental = new BookRentedEvent(newRentalId, userId, isbn, DEFAULT_RENTAL_LENGTH);

        log.info("Renting book {}...", isbn);
        eventRepository.addEvent(newRental);
        rabbitTemplate.convertAndSend(config.topicExchange().name(), "rental.rent", newRental);
        return newRental.getRentalId();
    }

    public void returnBook(UUID rentId)  {
        var returnBook = new BookReturnedEvent(rentId);

        log.info("Returning book on rent with id {}...", rentId);
        eventRepository.addEvent(returnBook);
        rabbitTemplate.convertAndSend(config.topicExchange().name(), "rental.return", returnBook);
    }
}
