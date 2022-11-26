FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy .csproj files and restore project dependencies
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /src
COPY *.sln .

COPY Bookstore.Core/*.csproj ./Bookstore.Core/
COPY Bookstore.Interfaces/*.csproj ./Bookstore.Interfaces/
COPY Bookstore.BusinessLogic/*.csproj ./Bookstore.BusinessLogic/
COPY Bookstore.DataAccess/*.csproj ./Bookstore.DataAccess/
COPY Bookstore.DataAccessMock/*.csproj ./Bookstore.DataAccessMock/
COPY Bookstore.Api/*.csproj ./Bookstore.Api/
RUN dotnet restore "./Bookstore.Api/Bookstore.Api.csproj"

# Copy everything else to our current working directory and publish
COPY . .
RUN dotnet publish "./Bookstore.Api/Bookstore.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "Bookstore.Api.dll"]
