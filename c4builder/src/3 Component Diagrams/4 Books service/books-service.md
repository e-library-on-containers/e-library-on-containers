**Level 3: Component diagram for *Books* service**

*Books* service allows performing actions related to books (adding, deleting and editing books and their physical copies) and reading aggregated data from database.

*CQRS* (Command Query Responsibility Segregation) is used for reduced coupling between facade (controllers) and internal business logic services. 

According to *CQS* (Command-query separation) principles, in the system *Queries* retrieves data from database and processes it before returning it to user (without any side effects on system state) and *Commands* performs actions that changes state of the system without returning any data.

*RabbitMQ* is used to receive events about books rentals and returns from the Rentals service and about book data modifications taking place in the Books service. The contents of the Books Read database are updated on the basis of received messages.

*Books Read* database contains flattened data about the book and aggregated data about the number of copies available
