# Bid Calculation Service

This application uses Vue.js, tailwindcss for the frontend and ASP.NET Core API for the backend.

## API

The backend API utilizes the CQRS pattern and clean code principles using MediatR. Authentication is handled using JWT tokens. Unit tests are located in the `BidCalculationService.test` directory.

For fee calculation, the unit tests are located at `BidCalculationService.test/infrastructure/feeCalculatorServiceTest`.

## Database

PostgreSQL is used as the database with Entity Framework Core as the ORM.

## API Endpoints

### Account

- **POST** `/api/account/register`: Register User API
- **POST** `/api/account/login`: Login API

### Bid Calculator

- **POST** `/api/bid`: Calculate Bid API
- **GET** `/api/bid/list`: Get Bids calculated paginated by logged-in user
- **GET** `/api/bid/{id}`: Get Bid by ID

## Startup the Application

To start the application, follow these steps:

1. Run `docker-compose build`.
2. Run `docker-compose up`.
3. Navigate to [http://localhost:8080/](http://localhost:8080/).
