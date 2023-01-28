# Books service

## Tech stack

* .NET 7
* ASP<span>.</span>NET Core
* Docker

## Main libraries

* MediatR
* Dapper
* Npgsql
* RabbitMQ
 
## Architectural approach
This service Was developed according to 3-tier architecture. This architecture organizes application into 3 logical tiers: presentation tier, business tier (or application tier) and data Infrastructure (or data tier).

### Books.Api (presentation tier)
Api tier is exposing Web Api and using MediatR redirects requests to appropriate handlers.

### Books.Business (business tier)
Business tier delivers queries and commands for API and contains commands and queries handlers and RabbitMQ Producer and Worker. The producer, when invoking the command using RabbitMQ, sends a message that is consumed by the worker who updates the Read database. The worker also consumes book rental and return events sent by the Rental Service.

### Books.Infrastructure (infrastructure tier)
Infrastructure tier is responsible for communication with database. This tier contains repositories and data models. Repositories using the Dapper library allow the rest of the application to read and write to the database using models. Before saving from the model properties, an SQL query is created. During reading, the read data is mapped to models. 