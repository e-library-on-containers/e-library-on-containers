#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["BooksPresentation/Books.API.csproj", "BooksPresentation/"]
COPY ["BooksBusiness/Books.Business.csproj", "BooksBusiness/"]
COPY ["BooksInfrastructure/Books.Infrastructure.csproj", "BooksInfrastructure/"]
RUN dotnet restore "BooksPresentation/Books.API.csproj"
COPY . .
WORKDIR "/src/BooksPresentation"
RUN dotnet build "Books.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Books.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Books.API.dll"]