$filteredArgs = $args | Where-Object { $_ -ne "-l" }
if ($args -contains "-l") {
	& docker-compose -f docker-compose.yml -f docker-compose.dev.yml -f docker-compose.local.yml --env-file .dev-env $filteredArgs
} else {
	& docker-compose -f docker-compose.yml -f docker-compose.dev.yml --env-file .dev-env $filteredArgs
}
