# Bid Calculation Service

This application uses Vue.js for the frontend and ASP.NET Core API for the backend.

## API

The backend API utilizes the CQRS pattern and clean code principles using MediatR. Authentication is handled using JWT tokens.

## Database

PostgreSQL is used as the database with Entity Framework Core as the ORM.

## API Endpoints

### Account

- **POST** `/api/account/register`: Register User API
- **POST** `/api/account/login`: Login API

### Bid Calculator

- **POST** `/api/bid`: Calculate Bid API
- **GET** `/api/bid/list`: Get Bids calculated paginated
- **GET** `/api/bid/{id}`: Get Bids by ID
