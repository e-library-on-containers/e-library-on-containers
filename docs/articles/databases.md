# Database schemas

## RDBMS
Relational database management system (*RDBMS*) used in this project is **PostgreSQL**.

## Identity
![identity-erd](https://user-images.githubusercontent.com/48659621/215188955-7ba17225-5485-4638-9e55-44c9267abdb4.png)

Identity database stores information about users accounts and their roles. 
Due to use of *DbUp* library, *schemaversions* table is used for saving information about applied SQL scripts/migrations.

## Books
![books-erd](https://github.com/e-library-on-containers/e-library-on-containers/assets/48659621/331e783f-b90b-498e-b532-86c0b6aeacdf)

Books database stores information about books, audiobooks and their physical copies. To ensure avoiding n+1 query problem and possible heavy joins, redundant *booksread* table is used for querying information. All physical copies are aggregated and stores among other **Book** properties. 
Due to use of *DbUp* library, *schemaversions* table is used for saving information about applied SQL scripts/migrations.

## Rentals

![rentals-erd](https://user-images.githubusercontent.com/48659621/215188932-4170652d-0631-493d-83ac-733292de7489.png)

Rentals database stores information about books rentals. All rent/extend/return events are stored in *event* table, working as an event store for event sourcing, enabling auditability and easy access to user's history. Current rentals are projected and stored in *rental* table.
Due to use of *liquibase* library, *databasechangelog* and *databasechangeloglock* tables are used for saving information about applied SQL scripts/migrations.

## Movies

![movies-erd](https://github.com/e-library-on-containers/e-library-on-containers/assets/48659621/55b40968-07be-404f-b5af-e95083da2785)

Movies database stores information about movies (its title, category, duration), including details about people involved in the movie-making process such as actors, directors, and screenwriters.
Due to use of *DbUp* library, *schemaversions* table is used for saving information about applied SQL scripts/migrations.