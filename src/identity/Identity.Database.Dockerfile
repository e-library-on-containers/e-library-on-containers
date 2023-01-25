FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /build

COPY ./database/Identity.Database/Identity.Database.csproj ./
RUN dotnet restore 

COPY ./database/Identity.Database/ .
RUN dotnet publish -o /publish 

WORKDIR /publish 

CMD ["sh", "-c", "dotnet Identity.Database.dll \"${Database_ConnectionString}\""]