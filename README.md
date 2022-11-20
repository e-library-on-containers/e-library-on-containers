
[![LinkedIn][linkedin-shield-kulik]][linkedin-url-kulik] [![LinkedIn][linkedin-shield-swislocki]][linkedin-url-swislocki] [![LinkedIn][linkedin-shield-zajaczkowski]][linkedin-url-zajaczkowski]

[![Build and publish FunctionalValidation][build-publish-functional-validation-shield]][build-publish-functional-validation-url] 

 [![Build and deploy][build-deploy-docs-shield]][build-deploy-docs-url] 

# eLibraryOnContainers

## Table of Contents

* [About the Project](#about-the-project)
* [Internal Libraries](#internal-libraries)
	* [Functional Validation](#functional-validation)
* [Contributions](#contributions)

## About the project
*Project in development*

System for library that allows browsing and borrowing books through web. Project is designed and developed using microservices architecture with emphasis on **completely asynchronous** communication between services (using RabbitMQ message queue). Documentation is in constant development. Nevertheless, some description and diagrams (using UML with C4 Model) are already available in [docs](https://e-library-on-containers.github.io/e-library-on-containers/articles/diagrams.html).

## Internal Libraries
All internal libraries are published as NuGet packages in this repository's feed. Each library has its own workflows for building, testing and publishing new version.
Due to libraries internal use only, we decided that maintaining SemVer is not a must, so convention for libraries version names is `{year}.{month}.{day}.{run_number}`.

### Functional Validation
Library for strongly-typed validation rules that uses concept of monad implemented as **Result<TLeft, TRight>** (also known as **Either<TLeft, TRight>**). API exposed through FunctionalValidation library is documented using XML docs and published in [documentation](https://e-library-on-containers.github.io/e-library-on-containers/api/index.html) under *Api Documentation* tab.

## Contributions
If you want to contribute to this project see [CONTRIBUTING](CONTRIBUTING.md).

[linkedin-shield-zajaczkowski]: https://img.shields.io/badge/LinkedIn-Zajączkowski-blue?logo=linkedin
[linkedin-url-zajaczkowski]: https://www.linkedin.com/in/krzysztof-m-zajaczkowski/
[linkedin-shield-kulik]: https://img.shields.io/badge/LinkedIn-Kulik-blue?logo=linkedin
[linkedin-url-kulik]: https://www.linkedin.com/in/%E2%98%95-rafa%C5%82-kulik-12733a189/
[linkedin-shield-swislocki]: https://img.shields.io/badge/LinkedIn-Świsłocki-blue?logo=linkedin
[linkedin-url-swislocki]: https://www.linkedin.com/in/jakub-swislocki/
[build-deploy-docs-shield]: https://img.shields.io/github/workflow/status/e-library-on-containers/e-library-on-containers/Publish%20docs%20on%20GitHub%20Pages?label=Build%20and%20deploy%20docs&logo=GitHub
[build-deploy-docs-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/publish-docs.yml
[build-publish-functional-validation-shield]: https://img.shields.io/github/workflow/status/e-library-on-containers/e-library-on-containers/FunctionalValidation-PublishPackage?label=Publish%20FunctionalValidation%20package&logo=GitHub
[build-publish-functional-validation-url]: https://github.com/e-library-on-containers/e-library-on-containers/actions/workflows/functional-validation-publish.yml