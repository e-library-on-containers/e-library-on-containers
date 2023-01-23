FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

COPY ./Gateway.Api/Gateway.Api.csproj ./Gateway.Api/Gateway.Api.csproj

RUN dotnet restore ./Gateway.Api/Gateway.Api.csproj

COPY . .

FROM build AS publish
RUN dotnet publish ./Gateway.Api/Gateway.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Gateway.Api.dll"]