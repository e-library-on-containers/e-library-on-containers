FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine
WORKDIR /build

COPY ./Movies.Database/Movies.Database.csproj ./
RUN dotnet restore 

COPY ./Movies.Database/ .
RUN dotnet publish -o /publish 

WORKDIR /publish 

CMD ["sh", "-c", "dotnet Movies.Database.dll \"${Database_ConnectionString}\""]