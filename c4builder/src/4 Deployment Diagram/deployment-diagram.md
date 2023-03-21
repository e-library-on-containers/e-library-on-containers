**Deployment diagram**

Deployment diagram shows physical division of components throughout project's infrastructure. For testing environment, all components and databases are deployed as docker containers with specified image and version. Each container that is an internal part of this project is shown with its main artifact - service or library that is ran or executed as container's entrypoint.

Remarks:

*Database updaters (for Books and Identity service) are infrastructure containers. They do not provide any business functionalities, they are only used for applying database migrations (they will not be included in production environment and are not part of system functionalities so they're not included on other diagrams). In other environments databases will be durable (saved between deployments) and migrations will be applied on these databases as a different deployment step, and not as docker containers, in final deployment.*

*To minimize load on testing environment, write and read databases for each service are stored inside the same database container.*

*Since RabbitMq and Postgres are third-party containers, their artifacts weren't included (their internal structure is not part of this project).*

