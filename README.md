# Northwind Assessment

## Overview

This project implements an ASP.NET Core Web API against the Northwind sample database.

The API allows staff members to:

* View customers and their order counts
* Search customers by name
* View customer details and order history
* Review order totals and product counts

The solution demonstrates a layered architecture with separation of concerns, caching, observability, pagination, and automated testing.

## Technology Stack

* .NET 8
* ASP.NET Core Web API
* Razor Pages
* Dapper
* SQL Server LocalDB
* Swagger / OpenAPI
* IMemoryCache
* OpenTelemetry
* xUnit
* FluentAssertions
* Moq

## Project Structure

### Northwind.Api

ASP.NET Core Web API project containing controllers, middleware, dependency injection, and application configuration.

### Northwind.Application

Application layer containing business services, DTOs, interfaces, and shared models.

### Northwind.Infrastructure

Infrastructure layer containing repositories, database access, and SQL queries.

### Northwind.Web

Minimal Razor Pages front-end consuming the API over HTTP via `HttpClient`.

### Northwind.Tests

Unit and integration tests.

## Architecture

The solution follows a layered architecture:

* API layer responsible for HTTP endpoints and middleware
* Application layer containing business logic and orchestration
* Infrastructure layer responsible for data access
* Test project containing unit and integration tests

Dependency injection is configured through dedicated extension methods.

## API Endpoints

### Get Customers

GET /api/customers

Optional parameters:

* search
* page
* pageSize

Examples:

GET /api/customers?search=alf

GET /api/customers?page=2&pageSize=20

### Get Customer Details

GET /api/customers/{id}

Example:

GET /api/customers/ALFKI

Returns customer information together with order history summary.

## Logging

The application uses ASP.NET Core built-in `ILogger` abstractions.

* In development, logs are written to the console and Visual Studio output.
* Structured logging is used throughout the application layer.
* The logging implementation can be extended with providers such as Serilog or Application Insights.

## Caching

Customer list queries are cached using `IMemoryCache` to reduce database access and improve performance.

The cache uses expiration policies and can be replaced with distributed cache implementations such as Redis in production environments.

## Observability

The application uses OpenTelemetry metrics for observability.

Current instrumentation includes:

* ASP.NET Core request metrics
* .NET runtime metrics

Metrics are exported to the console and can be integrated with systems such as Prometheus, Grafana, or Azure Monitor.

## Health Checks

A health endpoint is exposed at:

GET /health

This endpoint can be used by monitoring systems and orchestration platforms.

## Error Handling

The application uses centralized exception handling middleware to ensure consistent error responses.

## Pagination

Customer listing supports pagination using:

* page
* pageSize

Results are returned using a generic `PagedResult<T>` model containing:

* Items
* Page
* PageSize
* TotalCount
* TotalPages

## Testing

Run:

dotnet test

The solution contains:

* Repository integration tests against a local Northwind database
* Controller unit tests using Moq
* Service layer unit tests covering business logic, pagination, and caching

## Assumptions and Trade-offs

* The number of products in an order is calculated as the number of distinct products included in the order.
* SQL Server LocalDB was chosen to keep local setup simple.
* Repository methods return DTOs directly to keep the solution lightweight and focused on the assessment requirements.
* In-memory caching was selected for simplicity; distributed caching would be preferred in production.

## Design Decisions

* Dapper was selected instead of Entity Framework Core because the solution requires a small number of read-only queries and benefits from explicit SQL control.
* A repository abstraction was used to isolate data access concerns.
* A dedicated application layer was introduced to separate business logic from infrastructure concerns.
* Swagger is enabled for easy API exploration and testing.

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
3. Configure the API base address in `Northwind.Web`
4. Start both `Northwind.Api` and `Northwind.Web`
5. Open:

   * Swagger UI at `/swagger`
   * Razor UI at `/Customers`

### Multiple Startup Projects (Visual Studio)

To run the complete solution:

1. Right-click the solution
2. Select **Set Startup Projects**
3. Choose **Multiple startup projects**
4. Set both:

   * `Northwind.Api` → Start
   * `Northwind.Web` → Start
	
## Front-End

A lightweight Razor Pages front-end was added to provide a simple end-to-end user experience and demonstrate HTTP-based integration with the API.

The front-end communicates exclusively with the Web API using `HttpClient` and does not access the database directly.

Implemented pages:

### Customers

Route:

`/Customers`

Displays:

* Customer ID
* Company name
* Order count
* Pagination information

Customer names link to the details page.

### Customer Details

Route:

`/Customer/{id}`

Displays:

* Customer information
* Contact name
* Country
* Order history
* Order totals
* Product counts

The application root (`/`) redirects to `/Customers`.

## Future Improvements

* Distributed caching with Redis
* Integration test database isolation
* API versioning
* Containerization with Docker

## AI Assistance

AI tools (ChatGPT) were used during development for:

* SQL query refinement
* Documentation and README review
* Code review
* Identifying and resolving floating-point precision issues by casting order totals to DECIMAL in SQL
* Suggestions regarding architecture, caching, observability, and testing
* The final implementation, testing, debugging, and integration of all code were performed manually.

### Example prompts

* "Create a Dapper query that returns customers with their order count."
* "Why am I getting floating-point precision issues in my order total calculations?"
* "Provide example xUnit tests for an ASP.NET Core controller using Moq."
* "Review this README and suggest improvements."
* "How should pagination and caching be implemented in a .NET Web API?"
