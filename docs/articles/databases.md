# Database schemas

## RDBMS
Relational database management system (*RDBMS*) used in this project is **PostgreSQL**.

## Identity
![identity-erd](https://user-images.githubusercontent.com/48659621/215188955-7ba17225-5485-4638-9e55-44c9267abdb4.png)

Identity database stores information about users accounts and their roles. Due to use of *DbUp* library, schemaversions table is used for saving information about applied SQL scripts/migrations.

## Books
![books-erd](https://user-images.githubusercontent.com/48659621/215188947-bbe01932-0086-454b-af6c-572474a8d791.png)

Books database stores information about books and their physical copies. To ensure avoiding n+1 query problem and possible heavy joins, redundant *booksread* table is used for querying information. All physical copies are aggregated and stores among other **Book** properties.

## Rentals

![rentals-erd](https://user-images.githubusercontent.com/48659621/215188932-4170652d-0631-493d-83ac-733292de7489.png)

Rentals database stores information about books rentals. All rent/extend/return events are stored in *event* table, working as an event store for event sourcing, enabling auditability and easy access to user's history. Current rentals are projected and stored in *rental* table.
Due to use of *liquibase* library, *databasechangelog* and *databasechangeloglock* tables are used for saving information about applied SQL scripts/migrations.