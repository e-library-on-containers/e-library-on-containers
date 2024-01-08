# Containerization

## Services

All services are containerized using Docker. For quick and easy deployments we introduced Continuous Delivery for our system. After merging pull requests to develop, all modified services are rebuilt and pushed to [Dockerhub](https://hub.docker.com/u/elibraryoncontainers).

## Docker compose

To simplify development, testing and releases, we used 3 docker-compose files. 
- *docker-compose.yml* - file with base services and networks definitions
- *docker-compose.dev.yml* - override with containerized databases and port mappings so that all services are accessible directly by developer
- *docker-compose.local.yml* - override that uses locally built images for all services, rather than images from remote Dockerhub repository

Along with these compose files, .dev-env (or .env) file is required to povide environment variables for all services. 

Also, we've introduced *dev-compose.ps1* and *dev-compose.sh* script files for easier use of docker-compose files. These scripts always run docker-compose with *docker-compose.yml* as base file, *docker-compose.dev.yml* as override and *.dev-env* as environment file. Also, both scripts have **-l** flag that uses *docker-compose.local.yml* as second override.