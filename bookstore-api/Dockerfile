FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy .csproj files and restore project dependencies
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /src
COPY *.sln .

COPY Bookstore.Core/*.csproj ./Playmate.Social.Domain/
COPY Bookstore.Interfaces/*.csproj ./Playmate.Social.Domain/
COPY Bookstore.BusinessLogic/*.csproj ./Playmate.Social.Domain/
COPY Bookstore.DataAccess/*.csproj ./Playmate.Social.Application/
COPY Bookstore.DataAccessMock/*.csproj ./Playmate.Social.Infrastructure/
COPY Playmate.Api/*.csproj ./Playmate.Social.WebAPI/
RUN dotnet restore "./Playmate.Social.WebAPI/Playmate.Social.WebAPI.csproj"

# Copy everything else to our current working directory and publish
COPY . .
RUN dotnet publish "./Playmate.Social.WebAPI/Playmate.Social.WebAPI.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "Playmate.Social.WebAPI.dll"]