package e.library.on.containers.rentals.service;

import e.library.on.containers.rentals.utils.events.BookRentedEvent;
import e.library.on.containers.rentals.utils.events.BookReturnedEvent;
import e.library.on.containers.rentals.utils.RentalsReadDao;
import e.library.on.containers.rentals.repository.RentalsReadRepository;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Component;

@Slf4j
@Component
class RentalsWorker {
	private final RentalsReadRepository readRepository;

	RentalsWorker(RentalsReadRepository readRepository) {
		this.readRepository = readRepository;
	}

	@RabbitListener(queues = "${rabbitmq.rent-queue.name}")
	public void receiveMessage(BookRentedEvent event) {
		var dao = RentalsReadDao.from(event);
		readRepository.insertRental(dao);
		log.info("Received: %s".formatted(event));
	}

	@RabbitListener(queues = "${rabbitmq.return-queue.name}")
	public void receiveMessage(BookReturnedEvent event) {
		var rentalId = event.rentalId();
		boolean isRemoved = readRepository.removeRental(rentalId);
		if (!isRemoved) {
			log.info("There were no rental with id {}", rentalId);
		}
		log.info("Received: %s".formatted(event));
	}
}
