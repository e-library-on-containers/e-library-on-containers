package e.library.on.containers.rentals.message;

import e.library.on.containers.rentals.configuration.RabbitmqProperties;
import e.library.on.containers.rentals.events.AcceptBorrowEvent;
import e.library.on.containers.rentals.events.BorrowRequestEvent;
import e.library.on.containers.rentals.events.Event;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.stereotype.Component;

@Slf4j
@Component
@RequiredArgsConstructor
public class RabbitMqBorrowEventSender implements BorrowEventSender {

    private final RabbitmqProperties config;
    private final RabbitTemplate rabbitTemplate;

    @Override
    public void send(BorrowRequestEvent event) {
        sendMessage("borrows.borrow", event);
    }

    @Override
    public void send(AcceptBorrowEvent event) {
        sendMessage("borrows.accept", event);
    }

    private void sendMessage(String routingKey, Event message) {
        log.debug("Sending message {}", message.toString());
        rabbitTemplate.convertAndSend(config.borrowsExchange().name(), routingKey, message);
    }
}
