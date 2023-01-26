FROM gradle:7.6.0-jdk17-alpine AS BUILDER

WORKDIR /source

COPY . /source

RUN gradle bootJar
RUN mkdir ../app
RUN mv ./build/libs/rentals-0.0.1.jar ../app/app.jar

FROM gcr.io/distroless/java17-debian11 AS TARGET

WORKDIR app
COPY --from=BUILDER app /app

EXPOSE 8080
ENTRYPOINT ["java", "-jar", "app.jar"]
