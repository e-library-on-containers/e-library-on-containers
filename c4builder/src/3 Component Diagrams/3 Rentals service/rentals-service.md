**Level 3: Component diagram for *Rentals* service**

*Rentals* service is responsible for all actions related to the rental of books. The service saves each borrowing as an event into the database and the worker creates a projection that.

According to *CQS* (Command-query separation) principles, in the system *Queries* retrieves data from database and processes it before returning it to user (without any side effects on system state) and *Commands* performs actions that changes state of the system without returning any data.

According to *Event Sourcing* pattern *Rentals Repository* is appended only database in which none record can be edited. *Rentals READ Repository* is projection made by *Rentals worker* which aggregated every event to projection that facilitates reading from the database.
