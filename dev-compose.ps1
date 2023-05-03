param (
    [Parameter(Mandatory=$false)]
    [switch]$l,
	[Parameter(ValueFromRemainingArguments=$true, Mandatory=$false)]
    [string[]]$OtherParams
    )

if ($l) {
	& docker-compose -f docker-compose.yml -f docker-compose.dev.yml -f docker-compose.local.yml --env-file .dev-env $OtherParams
} else {
	& docker-compose -f docker-compose.yml -f docker-compose.dev.yml --env-file .dev-env $OtherParams
}
