package e.library.on.containers.rentals;

import e.library.on.containers.rentals.events.Event;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.stereotype.Service;

import java.util.UUID;

@Slf4j
@Service
class RentalService {

	private final RabbitmqProperties config;
	private final RabbitTemplate rabbitTemplate;

	RentalService(RabbitmqProperties config, RabbitTemplate rabbitTemplate) {
		this.config = config;
		this.rabbitTemplate = rabbitTemplate;
	}

	UUID rentBook(String isbn) {
		var newRentalId = UUID.randomUUID();
		var newRental = new Event.BookRented(newRentalId, isbn);
		log.info("Renting book {}...", isbn);
		rabbitTemplate.convertAndSend(config.topicExchange().name(), "rental.rent", newRental);

		return newRental.getRentalId();
	}

	UUID returnBook(UUID rentId) {
		var returnBook = new Event.BookReturned(rentId);
		log.info("Returning book on rent with id {}...", rentId);
		rabbitTemplate.convertAndSend(config.topicExchange().name(), "rental.return", returnBook);

		return returnBook.getBookReturned();
	}
}
