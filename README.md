# Northwind Assessment

## Overview

This project implements a simple ASP.NET Core Web API against the Northwind sample database.

The API allows staff members to:

* View customers and their order counts
* Search customers by name
* View customer details and order history
* Review order totals and product counts

## Technology Stack

* .NET 8
* ASP.NET Core Web API
* Dapper
* SQL Server LocalDB
* Swagger / OpenAPI
* xUnit
* FluentAssertions
* Moq

## Project Structure

### Northwind.Api

ASP.NET Core Web API project containing controllers and application configuration.

### Northwind.Infrastructure

Data access layer containing repositories, DTOs and SQL queries.

### Northwind.Tests

Unit and integration tests.

## API Endpoints

### Get Customers

GET /api/customers

Optional search parameter:

GET /api/customers?search=alf

### Get Customer Details

GET /api/customers/{id}

Example:

GET /api/customers/ALFKI

Returns customer information together with order history summary.

## Running the Application

### Prerequisites

* .NET 8 SDK
* SQL Server LocalDB
* Northwind sample database

## Database Setup

1. Install SQL Server LocalDB
2. Create database `Northwind`
3. Execute scripts from `/scripts`

### Run

1. Restore NuGet packages
2. Ensure the Northwind database exists in LocalDB
3. Run the Northwind.Api project
4. Open Swagger UI at /swagger

## Logging

*The application uses ASP.NET Core built-in ILogger abstractions.
*In development, logs are written to the console and Visual Studio output.
*The logging implementation can be extended with providers such as Serilog or Application Insights.

## Testing

Run:

dotnet test

* Repository integration tests require a local Northwind database.
* Controller tests are implemented as unit tests using Moq.

## Assumptions and Trade-offs

* The number of products in an order is calculated as the number of distinct products included in the order.
* SQL Server LocalDB was chosen to keep local setup simple.
* Repository methods return DTOs directly to keep the solution lightweight and focused on the assessment requirements.

## Design Decisions

* Dapper was selected instead of Entity Framework Core because the solution requires a small number of read-only queries and benefits from explicit SQL control.
* A repository abstraction was used to isolate data access concerns.
* Swagger is enabled for easy API exploration and testing.

## Future Improvements

* Pagination for customer lists
* Structured logging
* Integration test database isolation
* API versioning
* Additional validation and error handling
* Centralized exception handling middleware
* Request validation

## AI Assistance

AI tools (ChatGPT) were used during development for:

* SQL query refinement
* Documentation and README review
* Code review
* Identifying and resolving floating-point precision issues by casting order totals to DECIMAL in SQL
* The final implementation, testing, debugging, and integration of all code were performed manually.

### Example prompts

* "Create a Dapper query that returns customers with their order count."
* "Why am I getting floating-point precision issues in my order total calculations?"
* "Provide example xUnit tests for an ASP.NET Core controller using Moq."
* "Review this README and suggest improvements."
