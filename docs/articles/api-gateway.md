# API Gateway

API Gateway is an ASP.NET Core API utilizing Ocelot library as API Gateway. Using this service all resources are exposed through this API with clearly separated and descriptive *endpoints*, *authentication* and *authorization* guards.

## API Routes

| Http method |                         Route                        | Guard* | Minimum role |          Description         |
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
|    GET      |                       /rentals                       |   V   |     User     |         Get user rents       |
|    POST     |                     /rentals/rent                    |   V   |     User     |          Rent a book         |
|   DELETE    |             /rentals/**{rentId}**/return             |   V   |     User     |          Return book         |
|    POST     |             /rentals/**{rentId}**/extend             |   V   |     User     |        Extend due date       |

\*Does endpoint require user to be authenticated with Bearer token

## Routes definition

Gateway configuration (i.e. routes) is defined in **ocelot.json**/**ocelot.{Environment}.json** file. Each route definition consists of downstream path (receiving API route), scheme that will be used for proxying request, receiver's host and port, upstream path (gateway API route), upstream methods handled by this definition and authentication options that defines authentication guard for definition and required claims (authenticated user's properties, e.g. role).