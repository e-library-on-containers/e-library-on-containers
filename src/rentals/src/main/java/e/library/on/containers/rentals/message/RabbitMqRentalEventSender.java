package e.library.on.containers.rentals.message;

import e.library.on.containers.rentals.configuration.RabbitmqProperties;
import e.library.on.containers.rentals.events.BookExtendedEvent;
import e.library.on.containers.rentals.events.BookRentedEvent;
import e.library.on.containers.rentals.events.BookReturnApprovedEvent;
import e.library.on.containers.rentals.events.BookReturnedEvent;
import e.library.on.containers.rentals.events.Event;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.stereotype.Component;

@Slf4j
@Component
@RequiredArgsConstructor
public class RabbitMqRentalEventSender implements RentalEventSender {

    private final RabbitmqProperties config;
    private final RabbitTemplate rabbitTemplate;

    @Override
    public void send(BookRentedEvent event) {
        sendMessage("rental.rent", event);
    }

    @Override
    public void send(BookReturnedEvent event) {
        sendMessage("rental.return", event);
    }

    @Override
    public void send(BookExtendedEvent event) {
        sendMessage("rental.extend", event);
    }

    @Override
    public void send(BookReturnApprovedEvent event) {
        sendMessage("rental.approve", event);
    }

    private void sendMessage(String routingKey, Event message) {
        log.debug("Sending message {}", message.toString());
        rabbitTemplate.convertAndSend(config.topicExchange().name(), routingKey, message);
    }
}
