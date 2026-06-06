# Northwind Assessment

## Overview

A .NET 8 Web API solution built against the Northwind sample database.

The application provides customer overview and customer order history endpoints for internal staff usage.

## Technology Stack

* .NET 8
* ASP.NET Core Web API
* Dapper
* SQL Server LocalDB
* Swagger / OpenAPI
* xUnit

## Features

### Customer Overview

Returns a list of customers together with the number of orders they have placed.

Supports filtering by customer name.

Endpoint:

GET /api/customers

Example:

GET /api/customers?search=alf

### Customer Details

Returns customer information and a summary of the customer's order history.

For each order the API returns:

* Order Id
* Order Date
* Total Order Value
* Number of Products

Endpoint:

GET /api/customers/{id}

Example:

GET /api/customers/ALFKI

## Project Structure

* Northwind.Api - ASP.NET Core Web API
* Northwind.Infrastructure - Data access and repository layer
* Northwind.Tests - Unit tests

## Notes

This solution uses Dapper for lightweight data access and SQL Server LocalDB with the Northwind sample database.

## Future Improvements

* Additional automated tests
* Paging for customer lists
* Structured logging
* Integration tests
