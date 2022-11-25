package e.library.on.containers.rentals;

import e.library.on.containers.rentals.events.Event;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Component;

@Slf4j
@Component
class RentalWorker {
	@RabbitListener(queues = "${rabbitmq.rent-queue.name}")
	public void receiveMessage(Event.BookRented message) {
		log.info("Received: %s".formatted(message));
	}

	@RabbitListener(queues = "${rabbitmq.return-queue.name}")
	public void receiveMessage(Event.BookReturned message) {
		log.info("Received: %s".formatted(message));
	}
}
