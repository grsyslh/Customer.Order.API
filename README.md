# Customer Order API

## Overview
The Customer Order API is a .NET 8 application designed to manage customer orders, products, and related operations. It leverages various modern technologies and libraries to provide a robust and scalable solution.

## Technologies and Libraries

### Frameworks and Languages
- **.NET 8**: The latest version of the .NET framework.
- **C# 12.0**: The programming language used for development.

### Database
- **PostgreSQL**: A powerful, open-source object-relational database system.
- **Entity Framework Core**: An ORM (Object-Relational Mapper) for .NET to work with databases using .NET objects.

### Caching
- **Redis**: An in-memory data structure store, used as a database, cache, and message broker.

### Messaging
- **RabbitMQ**: A message broker that enables applications to communicate with each other and exchange information.

### Logging
- **Serilog**: A diagnostic logging library for .NET applications.

### API Documentation
- **Swagger / Swashbuckle**: Tools for generating interactive API documentation.

### Dependency Injection and Middleware
- **MediatR**: A simple, unambitious mediator implementation in .NET.
- **TechBuddy Middlewares**: Middleware for exception handling.

### Authentication and Authorization
- **API Key Authentication**: Custom authentication scheme using API keys.

### Other Libraries
- **AutoMapper**: A library to map objects to each other.
- **StackExchange.Redis**: A high-performance Redis client for .NET.
- **Microsoft OpenAPI**: For OpenAPI/Swagger generation.

## Project Structure
- **Order.API**: Contains the main API project.
- **Order.ApplicationService**: Contains application services and handlers.
- **Order.DataAccess**: Contains data access layer and repository implementations.
- **Order.Domain**: Contains domain entities and base classes.
- **Order.Repository**: Contains the database context and configurations.
- **Order.Queue**: Contains RabbitMQ consumer and producer services.
- **Order.Caching**: Contains Redis caching services.

## Getting Started
### Some Customers For Test
34dc96f2-bd4f-4d86-8a06-342b180a02ca	Salih G.	Test Adres Denizli	grsyslh@gmail.com
6c531a8d-01c1-4dad-be85-9aa638e6b6fe	Ahmet T.	Istanbul Updated Customer Address	ahmet.t*****@aktiftech.com	2025-02-02 06:17:25+00	

### Prerequisites
- .NET 8 SDK
- PostgreSQL
- Redis
- RabbitMQ
    
