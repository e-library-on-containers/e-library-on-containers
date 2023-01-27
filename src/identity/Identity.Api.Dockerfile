FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
ARG FEED_USERNAME=e-library-on-containers
ARG PERSONAL_ACCESS_TOKEN=pat
WORKDIR /src

COPY ./api/Identity.Api/Identity.Api.csproj ./api/Identity.Api/Identity.Api.csproj
COPY ./core/Identity.Application/Identity.Application.csproj ./core/Identity.Application/Identity.Application.csproj
COPY ./core/Identity.Domain/Identity.Domain.csproj ./core/Identity.Domain/Identity.Domain.csproj
COPY ./infrastructure/Identity.Infrastructure/Identity.Infrastructure.csproj ./infrastructure/Identity.Infrastructure/Identity.Infrastructure.csproj

RUN dotnet nuget add source --username $FEED_USERNAME --password $PERSONAL_ACCESS_TOKEN --store-password-in-clear-text --name github "https://nuget.pkg.github.com/e-library-on-containers/index.json"

RUN dotnet restore ./api/Identity.Api/Identity.Api.csproj

COPY . .

FROM build AS publish
RUN dotnet publish ./api/Identity.Api/Identity.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Identity.Api.dll"]