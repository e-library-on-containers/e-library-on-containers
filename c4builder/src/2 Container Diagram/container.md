**Level 2: Container diagram**

Librarians and Customers can interact with system using *Single Page Application* written in Angular.

All API calls are routed to appropriate services through *API Gateway* that handles user authentication using *Identity* API.

System's business logic is divided into bounded contexts, each serving a specific subset of functionalities. Communication between contexts is **strictly asynchronous** (not in a meaning of asynchronous calls inside application but globally asynchronous using fire-and-forget event publishing pattern).

In current state, system is based on contexts:
* Identity - responsible for managing user account and authentication
* Books - responsible for managing books catalog and physical copies collection
* Rentals - responsible for managing rentals and allowing users to borrow books
<!-- * Storage - responsible for managing books cover images stored in an external object storage -->