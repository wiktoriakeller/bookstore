FROM nginx:alpine AS base
WORKDIR /usr/share/nginx/html
EXPOSE 80

# Copy .csproj files and restore project dependencies
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /src
COPY *.sln .

COPY Bookstore.UI/*.csproj ./Bookstore.UI/
RUN dotnet restore ./Bookstore.UI/Bookstore.UI.csproj

# Copy everything else to our current working directory and publish
COPY . .
RUN dotnet publish ./Bookstore.UI/Bookstore.UI.csproj -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /usr/share/nginx/html
COPY --from=build-env /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf

ARG API=http://localhost:5000
RUN sed -i "/APIUrl/c\ \"APIUrl\": \"${API}\"" appsettings.json
