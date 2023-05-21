FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine
WORKDIR /build

COPY ./database/Books.Database/Books.Database/Books.Database.csproj ./
RUN dotnet restore 

COPY ./database/Books.Database/Books.Database/ .
RUN dotnet publish -o /publish 

WORKDIR /publish 

CMD ["sh", "-c", "dotnet Books.Database.dll \"${Database_ConnectionString}\""]