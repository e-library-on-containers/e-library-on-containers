
# Identity service

## Tech stack

* Java 17
* Spring 2.7.5
* Docker

## Main libraries

* Functionality:
    * Lombok
    * Jpa
    * Hibernate
    * Liquibase
    * Logback

### Common
Common contains mappers for data access object and JPA Entities to make it easier to work with Database and have immutable objects everywhere.

### Configuration
Configuration contains all the files related to Spring configuration like `@Configuration` which show Spring how `application-*.yml` should look like and configuration for all the Beans used in code.

### Events
Events contains definition of every event used by Rentals Service.

### Repository
Repository contains `interfaces` for Spring Data to manipulate on data in database. Also, there you can find all the data access model and entities definitions.

Configuration for database is in `Liquibase` in resources. Currently, there is only `master.xml` which should be replaced with file containing all the files with changes set.

### Service
Service is the core business logic in whole component. It contains how event should be published and consumed.

### Web
Web contains `@Controllers` and all request and response definitions.

## Pipelines

### Publishing Docker images
Publishing pipeline is triggered on pushes to develop (i.e. on merging pull requests). It builds docker image with tags based on commit tags and branch name and publishes it on Docker Hub.
