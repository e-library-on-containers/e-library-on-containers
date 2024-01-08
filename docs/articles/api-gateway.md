# API Gateway

API Gateway is an ASP.NET Core API utilizing Ocelot library as API Gateway. Using this service all resources are exposed through this API with clearly separated and descriptive *endpoints*, *authentication* and *authorization* guards.

## API Routes

| Http method |                         Route                        | Guard*| Minimum role |          Description         |
|    :---:    |                         :---                         | :---: |     :---:    |            :----:            |
|    POST     |                     /identity/auth                   |   X   |       -      |  Authenticate existing user  |
|    POST     |                  /identity/register                  |   X   |       -      |       Register new user      |
|    POST     |              /identity/change-password               |   V   |     User     | Change current user password |
|    GET      |                        /books                        |   X   |       -      |         Get all books        |
|    GET      |                     /books/**{ISBN}**                |   X   |       -      |           Get book           |
|    POST     |                        /books                        |   V   |     Admin    |         Add new book         |
|   DELETE    |                        /books                        |   V   |     Admin    |          Delete book         |
|    PUT      |                        /books                        |   V   |     Admin    |          Update book         |
|    GET      | /books-copies?isbn=**{ISBN}**&isAvailable=**{bool}** |   X   |       -      |        Get book copies       |
|    GET      |                   /books-copies/**{id}**             |   X   |       -      |         Get book copy        |
|    POST     |                     /books-copies                    |   V   |     Admin    |         Add book copy        |
|   DELETE    |                     /books-copies                    |   V   |     Admin    |        Delete book copy      |
|    PUT      |                     /books-copies                    |   V   |     Admin    |        Update book copy      |
|    GET      |                     /books/get-preview               |   V   |     Admin    |       Get books in preview   |
|    POST     |              /books/**{isbn}**/publish               |   V   |     Admin    |      Publish book by ISBN    |
|    POST     |                     /audiobooks                      |   V   |     Admin    |         Add new audiobook    |
|    GET      |                   /audiobooks/**{id}**               |   X   |       -      |         Get audiobook        |
|    POST     |             /audiobooks/**{id}**/publish             |   V   |     Admin    |      Publish audiobook by ID |
|    GET      |                       /rentals                       |   V   |     User     |         Get user rents       |
|    POST     |                     /rentals/rent                    |   V   |     User     |          Rent a book         |
|   DELETE    |             /rentals/**{rentId}**/return             |   V   |     User     |          Return book         |
|    POST     |             /rentals/**{rentId}**/extend             |   V   |     User     |        Extend due date       |
|    POST     |                       /movies                        |   V   |     Admin    |         Add new movie        |
|    GET      |                       /movies                        |   X   |       -      |         Get all movies       |
|   DELETE    |                    /movies/**{id}**                  |   V   |     Admin    |          Delete movie        |
|    GET      |                  /movies/**{id}**                    |   X   |       -      |           Get movie          |
|    POST     |               /movies/**{id}**/publish               |   V   |     Admin    |       Publish movie by ID    |
|    GET      |                       /people                        |   X   |       -      |         Get all people       |
|    POST     |                       /people                        |   V   |     Admin    |         Add new person       |
|   DELETE    |                  /people/**{id}**                    |   V   |     Admin    |        Delete person by ID   |

\*Endpoint requires user to be authenticated with Bearer token

## Routes definition

Gateway configuration (i.e. routes) is defined in **ocelot.json**/**ocelot.{Environment}.json** file. Each route definition consists of downstream path (receiving API route), scheme that will be used for proxying request, receiver's service name (to be used by Consul service discovery), upstream path (gateway API route), upstream methods handled by this definition and authentication options that defines authentication guard for definition and required claims (authenticated user's properties, e.g. role).

Additionally, Gateway utilizes Consul service discovery to dynamically resolve addresses, ensuring efficient and reliable communication with the registered services.