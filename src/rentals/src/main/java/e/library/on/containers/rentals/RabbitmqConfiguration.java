package e.library.on.containers.rentals;

import org.springframework.amqp.core.BindingBuilder;
import org.springframework.amqp.core.Declarables;
import org.springframework.amqp.core.Queue;
import org.springframework.amqp.core.TopicExchange;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.amqp.support.converter.Jackson2JsonMessageConverter;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
class RabbitmqConfiguration {

	@Bean
	Queue rentQueue(RabbitmqProperties config) {
		return new Queue(config.rentQueue().name(), false);
	}

	@Bean
	TopicExchange rentalsExchange(RabbitmqProperties config) {
		return new TopicExchange(config.topicExchange().name());
	}

	@Bean
	Queue returnQueue(RabbitmqProperties config) {
		return new Queue(config.returnQueue().name(), false);
	}

	@Bean
	Declarables bindings(RabbitmqProperties config) {
		return new Declarables(
				BindingBuilder.bind(rentQueue(config)).to(rentalsExchange(config)).with("rent"),
				BindingBuilder.bind(returnQueue(config)).to(rentalsExchange(config)).with("return")
		);
	}

	@Bean
	public Jackson2JsonMessageConverter producerJackson2MessageConverter() {
		return new Jackson2JsonMessageConverter();
	}

	@Bean
	public RabbitTemplate rabbitTemplate(final ConnectionFactory connectionFactory) {
		RabbitTemplate rabbitTemplate = new RabbitTemplate(connectionFactory);
		rabbitTemplate.setMessageConverter(producerJackson2MessageConverter());
		return rabbitTemplate;
	}
}
