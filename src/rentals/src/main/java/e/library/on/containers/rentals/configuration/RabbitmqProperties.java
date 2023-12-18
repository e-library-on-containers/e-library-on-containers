package e.library.on.containers.rentals.configuration;

import org.springframework.boot.context.properties.ConfigurationProperties;

@ConfigurationProperties(prefix = "rabbitmq")
public
record RabbitmqProperties(
	TopicExchange rentalsExchange,
	Queue rentQueue,
	Queue returnQueue,
	Queue extendQueue,
    Queue awaitingQueue,
	TopicExchange borrowsExchange,
	Queue borrowQueue,
	Queue acceptQueue
) {
	public record TopicExchange(String name){}
	public record Queue(String name){}
}
