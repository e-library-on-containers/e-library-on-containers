#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /src
COPY ["api/Books.Api/Books.Api.csproj", "api/Books.Api/"]
COPY ["core/Books.Core/Books.Core.csproj", "core/Books.Core/"]
COPY ["infrastructure/Books.Infrastructure/Books.Infrastructure.csproj", "infrastructure/Books.Infrastructure/"]
RUN dotnet restore "api/Books.Api/Books.Api.csproj"
COPY . .
WORKDIR "/src/api/Books.Api"
RUN dotnet build "Books.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Books.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Books.Api.dll"]