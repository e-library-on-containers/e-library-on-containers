
# Identity service

## Tech stack

* .NET 6
* ASP<span>.</span>NET Core
* Docker

## Main libraries

* Functionality:
	* MediatR
	* CSharpFunctionalExtensions
	* FunctionalValidation
	* Dapper
	* Npgsql
	* DbUp
* Testing
	* Machine.Specifications
	* Microsoft.AspNetCore.Mvc.Testing
	* Testcontainers
	* NSubstitute

## Architectural approach

Clean architecture was used an architectural approach to developing this service. Although one of its layers is called *Domain*, use of *Domain Driven Design* is not required. Nevertheless, some of its strategies were utilized here (i.e. value objects and business logic in domain model).

### Api
Api layers contains all classes related to ASP<span>.</span>NET Core and exposing Web API, i.e. request validators, non-business request/response DTOs, pipeline/middleware configuration etc.

### Infrastructure
Infrastructure layer contains logic related to communication with database (i.e. repository, read models) and authentication service. Read models are anemic models closely related to domain models, but lacking any rules and business logic. They are pure DTOs for easier data mapping from database. Authentication service is responsible for creating authentication bearer *JWT token* for user.

### Application
Application layers defines CQRS queries and commands request, their handlers and DTOs and business logic interfaces (services and repositories) used within this layer. 

### Domain
Domain layer contains definitions of domain models (*User* and *Role*), value objects related to these models and application errors (defining rules that must be followed.

### Database updater

Unrelated to Identity service in terms of code, this service is used for handling SQL scripts/migrations to database using *DbUp* library. Docker container checks schema state, applies SQL scripts/migrations if needed and exits.

## Coding style

Coding style heavily utilizes *CSharpFunctionalExtensions* library. There is no branching in business logic of this service apart from pipeline configuration and on the edges of service's logic (switch statement in controller on the user <-> api edge to specify error code and if checks on the api <-> database edge to encapsulate queried data into functional code.

## Tests

Testing in this project utilizes rarely used library and approach. *Machine.Specifications* is a Context/Specification testing framework using approach that is closely related to Behavior-Driven Development (*BDD*) because of conventions used when describing tests that resembles functionality and rules that affects it.

Example:
```
static IService service;
static bool result;

class When_service_returns_false
{
    Establish ctx = () => service.Call().ReturnsForAnyArgs(false)

    Because of = () => result = service.Call();

    It should_return_false = () => results.ShouldBeFalse();
}
```


### Unit tests

Only domain model entities and value objects are covered by unit tests. In current state, service doesn't have complicated dependencies and heavy logic in other layers, so such low coverage in unit tests is compensated in integration tests.

### Integration tests

In case of this service, which works as a backbone of entire system (providing user authentication and authorization), intregation tests are or heavy importance. For quick and easy test runs, *Microsoft.AspNetCore.Mvc.Testing* is used for starting up entire API in-memory and *Testcontainers* runs **PostgreSQL** containers with applied schema migrations. Both of these resources are disposed when cleanin up after the test.

## Pipelines

### Testing
Testing pipeline is triggered on pull requests containing changes to pipeline configuration or identity source code. It runs unit tests and integration tests after.

### Publishing Docker images
Publishing pipeline is triggered on pushes to develop (i.e. on merging pull requests). It builds docker image with tags based on commit tags and branch name and publishes it on Docker Hub.