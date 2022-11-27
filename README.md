# 📚 Bookstore

Web API and a Blazor client app for a bookstore.

# Technologies

- .NET 7
- ASP.NET Core
- FluentValidation
- AutoMapper
- Entity Framework Core
- Blazor
- HTML 5
- CSS 3
- Docker

# Setup

Create `.env` file in the `src` folder, it should contain the following variables:

- `SA_PASSWORD` - password to the database,
- `USE_MOCK_DATA_ACCESS` - boolean value, determines whether use mock or a sql database

Example file:

```
SA_PASSWORD=superPassword123
USE_MOCK_DATA_ACCESS=false
```

Then in the `src` folder run:

```
docker compose up
```

Web API URL: `localhost:5000/swagger`

Client app URL: `localhost:8000`

To delete created containers run:

```
docker compose down
```

To force rebuild of the containers run:

```
docker compose up --build
```
