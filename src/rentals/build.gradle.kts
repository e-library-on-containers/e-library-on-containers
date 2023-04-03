plugins {
	java
	kotlin("jvm").version("1.7.21")
	id("org.springframework.boot").version("2.7.5")
	id("io.spring.dependency-management").version("1.0.15.RELEASE")
	id("org.sonarqube").version("3.5.0.2730")
}

group = "e.library.on.containers"
version = "0.0.1"

repositories {
	mavenCentral()
}

dependencies {
	implementation("org.yaml:snakeyaml:1.33")
	implementation("org.springframework.boot:spring-boot-starter-actuator")
	implementation("org.springframework.boot:spring-boot-starter-web")
	implementation("org.springframework.boot:spring-boot-starter-amqp")
	implementation("com.fasterxml.jackson.datatype:jackson-datatype-jsr310:2.14.2")

	implementation("org.springframework.boot:spring-boot-starter-jdbc")
	implementation("org.springframework.boot:spring-boot-starter-data-jdbc")
	implementation("org.springframework.boot:spring-boot-starter-data-jpa")
	runtimeOnly("org.postgresql:postgresql:42.3.8")
	implementation("org.liquibase:liquibase-core:4.20.0")

	annotationProcessor("org.springframework.boot:spring-boot-configuration-processor")
	annotationProcessor("org.projectlombok:lombok:1.18.26")
	implementation("org.projectlombok:lombok:1.18.26")

	testImplementation("org.springframework.boot:spring-boot-starter-test")
	testImplementation("org.assertj:assertj-core:3.24.2")
	testImplementation("org.mockito:mockito-core:5.2.0")
}

tasks.getByName<Test>("test") {
	useJUnitPlatform()
}

sonarqube {
	properties {
		property("sonar.host.url", "http://localhost:9000")
		property("sonar.login", "admin")
		property("sonar.password","password")
		property("sonar.java.binaries", "**/*")
	}
}

java {
	sourceCompatibility = JavaVersion.VERSION_17
	targetCompatibility = JavaVersion.VERSION_17
}
