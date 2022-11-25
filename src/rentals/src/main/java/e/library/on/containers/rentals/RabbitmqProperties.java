package e.library.on.containers.rentals;

import org.springframework.boot.context.properties.ConfigurationProperties;

@ConfigurationProperties(prefix = "rabbitmq")
record RabbitmqProperties(
	TopicExchange topicExchange,
	Queue rentQueue,
	Queue returnQueue) {
	public record TopicExchange(String name){}
	public record Queue(String name){}
}
