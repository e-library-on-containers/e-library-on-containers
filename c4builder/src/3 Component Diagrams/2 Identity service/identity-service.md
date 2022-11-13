**Level 3: Component diagram for *Identity* service**

*Identity* service acts as a guard for entire system, managing user accounts infomation, providing authentication and allowing (or restricting) authenticated users access to parts of the system.

*CQRS* (Command Query Responsibility Segregation) is used for reduced coupling between facade (controllers) and internal business logic services. 

According to *CQS* (Command-query separation) principles, in the system *Queries* retrieves data from database and processes it before returning it to user (without any side effects on system state) and *Commands* performs actions that changes state of the system without returning any data.