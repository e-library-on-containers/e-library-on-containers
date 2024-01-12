#!/bin/bash

LOCAL_BUILD=false

while getopts "l" opt; do
  case ${opt} in
    l) LOCAL_BUILD=true ;;
    *) echo "Invalid option: -$OPTARG" >&2 ;;
  esac
done

if [ "$LOCAL_BUILD" = true ]
  then
    shift 
    docker compose -f docker-compose.yml -f docker-compose.dev.yml -f docker-compose.local.yml --env-file .dev-env "$@"
  else
    docker compose -f docker-compose.yml -f docker-compose.dev.yml --env-file .dev-env "$@"
fi
