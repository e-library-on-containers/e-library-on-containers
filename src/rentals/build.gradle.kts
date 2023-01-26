plugins {
	java
	kotlin("jvm").version("1.7.21")
	id("org.springframework.boot").version("2.7.5")
	id("io.spring.dependency-management").version("1.0.15.RELEASE")
}

group = "e.library.on.containers"
version = "0.0.1-SNAPSHOT"

repositories {
	mavenCentral()
}

dependencies {
	implementation("org.springframework.boot:spring-boot-starter-actuator")
	implementation("org.springframework.boot:spring-boot-starter-web")
	implementation("org.springframework.boot:spring-boot-starter-amqp")
	implementation("com.fasterxml.jackson.datatype:jackson-datatype-jsr310:2.14.1")

	implementation("org.springframework.boot:spring-boot-starter-jdbc")
	implementation("org.springframework.boot:spring-boot-starter-data-jdbc")
	implementation("org.springframework.boot:spring-boot-starter-data-jpa")
	runtimeOnly("org.postgresql:postgresql")
	runtimeOnly("com.h2database:h2")
	implementation("org.liquibase:liquibase-core:4.8.0")


	annotationProcessor("org.springframework.boot:spring-boot-configuration-processor")
	annotationProcessor("org.projectlombok:lombok:1.18.24")
	implementation("org.projectlombok:lombok:1.18.24")

	testImplementation("org.springframework.boot:spring-boot-starter-test")
}

tasks.getByName<Test>("test") {
	useJUnitPlatform()
}

java {
	sourceCompatibility = JavaVersion.VERSION_17
	targetCompatibility = JavaVersion.VERSION_17
}
