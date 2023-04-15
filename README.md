



[![LinkedIn][linkedin-shield-kulik]][linkedin-url-kulik] [![LinkedIn][linkedin-shield-swislocki]][linkedin-url-swislocki] [![LinkedIn][linkedin-shield-zajaczkowski]][linkedin-url-zajaczkowski]

[![Build and publish FunctionalValidation][build-publish-functional-validation-shield]][build-publish-functional-validation-url] 

 [![Build and deploy][build-deploy-docs-shield]][build-deploy-docs-url] 

# eLibraryOnContainers

## Table of Contents

* [Docker images](#docker-images)
* [About the Project](#about-the-project)
* [Internal Libraries](#internal-libraries)
	* [Functional Validation](#functional-validation)
* [Local environment](#local-environment)
* [Contributions](#contributions)

## Docker images
| Status| Service|
|:---:|:----:|
| [![build-publish-front-shield]][build-publish-front-url]  | Frontend |
| [![build-publish-gateway-api-shield]][build-publish-gateway-api-url]  | API Gateway |
| [![build-publish-identity-api-shield]][build-publish-identity-api-url]  | Identity API |
| [![build-publish-identity-updater-shield]][build-publish-identity-updater-url]  | Identity Database updater|
| [![build-publish-books-api-shield]][build-publish-books-api-url]  | Books API |
| [![build-publish-books-updater-shield]][build-publish-books-updater-url]  | Books Database updater|
| [![build-publish-rentals-api-shield]][build-publish-rentals-api-url]  | Rentals API |

## About the project
*Project in development*

System for library that allows browsing and borrowing books through web. Project is designed and developed using microservices architecture with emphasis on **completely asynchronous** communication between services (using RabbitMQ message queue). Documentation is in constant development. Nevertheless, some description and diagrams (using UML with C4 Model) are already available in [docs](https://e-library-on-containers.github.io/e-library-on-containers/articles/diagrams.html).

## Internal Libraries
All internal libraries are published as NuGet packages in this repository's feed. Each library has its own workflows for building, testing and publishing new version.
Due to libraries internal use only, we decided that maintaining SemVer is not a must, so convention for libraries version names is `{year}.{month}.{day}.{run_number}`.

### Functional Validation
Library for strongly-typed validation rules that uses concept of monad implemented as **Result<TLeft, TRight>** (also known as **Either<TLeft, TRight>**). API exposed through FunctionalValidation library is documented using XML docs and published in [documentation](https://e-library-on-containers.github.io/e-library-on-containers/api/index.html) under *Api Documentation* tab.

## Local environment

### Prerequisites
**Right now building images locally is available only for project contributors!**
For building images locally, GitHub personal access token with access to reading packages is required. Fill in these variables in `.dev-env` file:
```
FEED_USERNAME="<GITHUB_USERNAME>"
PERSONAL_ACCESS_TOKEN="<GITHUB_PTA>"
```
### Docker compose
To startup local development environment run `dev-compose.sh up` (Linux) or `dev-compose.ps1 up` (Windows).
API Gateway is mapped to `http://localhost:8080`.
Frontend is mapped to `http://localhost:8084 `.

## Contributions
If you want to contribute to this project see [CONTRIBUTING](CONTRIBUTING.md).

[linkedin-shield-zajaczkowski]: https://img.shields.io/badge/LinkedIn-Zajączkowski-blue?logo=linkedin
[linkedin-url-zajaczkowski]: https://www.linkedin.com/in/krzysztof-m-zajaczkowski/
[linkedin-shield-kulik]: https://img.shields.io/badge/LinkedIn-Kulik-blue?logo=linkedin
[linkedin-url-kulik]: https://www.linkedin.com/in/%E2%98%95-rafa%C5%82-kulik-12733a189/
[linkedin-shield-swislocki]: https://img.shields.io/badge/LinkedIn-Świsłocki-blue?logo=linkedin
[linkedin-url-swislocki]: https://www.linkedin.com/in/jakub-swislocki/
[build-deploy-docs-shield]: https://img.shields.io/github/actions/workflow/status/e-library-on-containers/e-library-on-containers/publish-docs.yml?label=Build%20and%20deploy%20docs&logo=GitHub
[build-deploy-docs-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/publish-docs.yml
[build-publish-functional-validation-shield]: https://img.shields.io/github/actions/workflow/status/e-library-on-containers/e-library-on-containers/functional-validation-publish.yml?label=Publish%20FunctionalValidation%20package&logo=GitHub
[build-publish-functional-validation-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/functional-validation-publish.yml

[build-publish-books-api-shield]: https://img.shields.io/github/actions/workflow/status/e-library-on-containers/e-library-on-containers/books-api-publish-docker.yml?label=Push%20to%20Docker%20Hub&logo=Docker
[build-publish-books-api-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/books-api-publish-docker.yml

[build-publish-books-updater-shield]: https://img.shields.io/github/actions/workflow/status/e-library-on-containers/e-library-on-containers/books-db-updater-publish-docker.yml?label=Push%20to%20Docker%20Hub&logo=Docker
[build-publish-books-updater-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/books-db-updater-publish-docker.yml

[build-publish-front-shield]: https://img.shields.io/github/actions/workflow/status/e-library-on-containers/e-library-on-containers/front-prod-publish-docker.yml?label=Push%20to%20Docker%20Hub&logo=Docker
[build-publish-front-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/front-prod-publish-docker.yml

[build-publish-gateway-api-shield]: https://img.shields.io/github/actions/workflow/status/e-library-on-containers/e-library-on-containers/gateway-api-publish-docker.yml?label=Push%20to%20Docker%20Hub&logo=Docker
[build-publish-gateway-api-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/gateway-api-publish-docker.yml

[build-publish-identity-api-shield]: https://img.shields.io/github/actions/workflow/status/e-library-on-containers/e-library-on-containers/identity-api-publish-docker.yml?label=Push%20to%20Docker%20Hub&logo=Docker
[build-publish-identity-api-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/identity-api-publish-docker.yml

[build-publish-identity-updater-shield]: https://img.shields.io/github/actions/workflow/status/e-library-on-containers/e-library-on-containers/identity-db-updater-publish-docker.yml?label=Push%20to%20Docker%20Hub&logo=Docker
[build-publish-identity-updater-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/identity-db-updater-publish-docker.yml

[build-publish-rentals-api-shield]: https://img.shields.io/github/actions/workflow/status/e-library-on-containers/e-library-on-containers/rentals-publish-docker.yml?label=Push%20to%20Docker%20Hub&logo=Docker
[build-publish-rentals-api-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/rentals-publish-docker.yml