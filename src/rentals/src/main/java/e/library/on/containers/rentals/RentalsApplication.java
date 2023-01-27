package e.library.on.containers.rentals;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.ConfigurationPropertiesScan;

@SpringBootApplication
@ConfigurationPropertiesScan
public class RentalsApplication {

	public static void main(String[] args) {
		SpringApplication.run(RentalsApplication.class, args);
	}

}
