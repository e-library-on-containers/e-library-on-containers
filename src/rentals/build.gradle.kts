import org.jetbrains.kotlin.gradle.tasks.KotlinCompile

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
	annotationProcessor("org.springframework.boot:spring-boot-configuration-processor")

	implementation("org.projectlombok:lombok:1.18.24")
	implementation("com.fasterxml.jackson.datatype:jackson-datatype-jsr310:2.14.1")
	annotationProcessor("org.projectlombok:lombok:1.18.24")

	testImplementation("org.springframework.boot:spring-boot-starter-test")
}

tasks.getByName<Test>("test") {
	useJUnitPlatform()
}

java {
	sourceCompatibility = JavaVersion.VERSION_17
	targetCompatibility = JavaVersion.VERSION_17
}

tasks.withType<KotlinCompile>().all {
	kotlinOptions.jvmTarget = "17"
}
