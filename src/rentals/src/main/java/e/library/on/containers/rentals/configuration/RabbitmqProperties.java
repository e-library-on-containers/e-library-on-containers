package e.library.on.containers.rentals.configuration;

import org.springframework.boot.context.properties.ConfigurationProperties;

@ConfigurationProperties(prefix = "rabbitmq")
public
record RabbitmqProperties(
	TopicExchange topicExchange,
	Queue rentQueue,
	Queue returnQueue,
	Queue extendQueue
) {
	public record TopicExchange(String name){}
	public record Queue(String name){}
}
