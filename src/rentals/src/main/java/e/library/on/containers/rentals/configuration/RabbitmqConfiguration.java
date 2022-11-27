package e.library.on.containers.rentals.configuration;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.SerializationFeature;
import com.fasterxml.jackson.databind.util.StdDateFormat;
import com.fasterxml.jackson.datatype.jdk8.Jdk8Module;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;
import com.fasterxml.jackson.module.paramnames.ParameterNamesModule;
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
				BindingBuilder.bind(rentQueue(config)).to(rentalsExchange(config)).with("rental.rent"),
				BindingBuilder.bind(returnQueue(config)).to(rentalsExchange(config)).with("rental.return")
		);
	}

	@Bean
	public Jackson2JsonMessageConverter producerJackson2MessageConverter(ObjectMapper objectMapper) {
		return new Jackson2JsonMessageConverter(objectMapper);
	}

	@Bean
	public ObjectMapper objectMapper() {
		ObjectMapper mapper = new ObjectMapper()
				.registerModule(new ParameterNamesModule())
				.registerModule(new Jdk8Module())
				.registerModule(new JavaTimeModule());
		mapper.disable(SerializationFeature.WRITE_DATES_AS_TIMESTAMPS);
		mapper.setDateFormat(new StdDateFormat());

		return mapper;
	}

	@Bean
	public RabbitTemplate rabbitTemplate(
			final ConnectionFactory connectionFactory,
			final ObjectMapper objectMapper) {
		RabbitTemplate rabbitTemplate = new RabbitTemplate(connectionFactory);
		rabbitTemplate.setMessageConverter(producerJackson2MessageConverter(objectMapper));
		return rabbitTemplate;
	}
}
