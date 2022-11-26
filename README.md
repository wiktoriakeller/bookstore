# Bookstore

Web API and a blazor client app for a bookstore.

# Setup

Create `.env` file in the root folder, it should contain the following variables:

- `SA_PASSWORD` - password to the database,
- `USE_MOCK_DATA_ACCESS` - boolean value, determines whether use mock or a sql database

Example file:

```
SA_PASSWORD=Your_password123
USE_MOCK_DATA_ACCESS=false
```

Then in the root folder run:

```
docker compose up
```

To delete created containers run:

```
docker compose down
```

To force rebuild of the containers run:

```
docker compose up --build
```
