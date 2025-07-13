# EventBookingSystem – Backend

A robust, scalable backend for an event booking platform, built with ASP.NET Core and Entity Framework Core. This API enables users to browse, book, and manage event reservations, with comprehensive admin features and secure authentication.

---

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Setup & Installation](#setup--installation)
  - [Database Migration](#database-migration)
  - [Running the API](#running-the-api)
- [API Documentation](#api-documentation)
- [Testing](#testing)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)
- [License](#license)

---

## Overview

This backend powers the EventBookingSystem, providing RESTful APIs for event management, user authentication, and booking operations. It supports both user-facing and admin functionalities, ensuring a seamless and secure experience.

---

## Architecture

- **ASP.NET Core Web API** – Main API layer
- **Entity Framework Core** – ORM for data access
- **Layered Solution** – Separation of concerns via Core, Repository, Service, and DTO layers
- **JWT Authentication** – Secure user and admin access
- **Unit Testing** – Ensures code reliability and maintainability

---

## Features

- User registration and JWT-based authentication
- Event browsing and search
- Booking creation and cancellation
- User-specific booking management
- Admin dashboard for event and booking administration
- API documentation via Swagger
- Comprehensive unit and integration tests

---

## Project Structure

```
EventBookingSystem/
│
├── AdminDashBoard/         # ASP.NET Core admin dashboard (UI & API)
├── Booking.Core/           # Core business models and interfaces
├── Booking.Repo/           # Data access layer (repositories, EF DbContext)
├── Bookings.Service/       # Business logic/services
├── DTOS/                   # Shared Data Transfer Objects
├── EventBookingSystem/     # Main ASP.NET Core Web API
├── frontend/               # Static frontend (HTML, CSS, JS)
├── unitTesting/            # xUnit test project
├── EventBookingSystem.sln  # Solution file
└── README.md               # Project documentation
```

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server (or update connection string for your preferred DB)

### Setup & Installation

1. **Clone the repository:**
   ```sh
   git clone <your-repo-url>
   cd EventBookingSystem
   ```

2. **Restore dependencies and build:**
   ```sh
   dotnet restore
   dotnet build
   ```

### Database Migration

1. Update your connection string in `appsettings.json` ([AdminDashBoard/appsettings.json](AdminDashBoard/appsettings.json) or [EventBookingSystem/appsettings.json](EventBookingSystem/appsettings.json)).
2. Apply migrations:
   ```sh
   dotnet ef database update --project EventBookingSystem/EventBookingSystem.csproj
   ```

### Running the API

```sh
dotnet run --project EventBookingSystem/EventBookingSystem.csproj
```
The API will be available at `https://localhost:5001` (or as configured).

---

## API Documentation

- Interactive API docs available at `/swagger` when the backend is running.
- Main endpoints:
  - `POST /api/auth/register` – Register a new user
  - `POST /api/auth/login` – Authenticate and receive JWT
  - `GET /api/events` – List all events
  - `POST /api/bookings` – Book an event
  - `GET /api/bookings/user/{userId}` – Get bookings for a user
  - `DELETE /api/bookings/{bookingId}` – Cancel a booking

> For full details, see controllers in [AdminDashBoard/Controllers/](AdminDashBoard/Controllers/) and [EventBookingSystem/Controllers/](EventBookingSystem/Controllers/).

---

## Testing

1. **Navigate to the test project:**
   ```sh
   cd unitTesting
   ```
2. **Run all tests:**
   ```sh
   dotnet test
   ```

---

## Technologies Used

- **Backend:** ASP.NET Core, Entity Framework Core, SQL Server
- **Authentication:** JWT
- **Testing:** xUnit, FluentAssertions, Moq, EFCore.InMemory
- **Other:** AutoMapper, Swagger

---

## Contributing

1. Fork the repository and create your branch (`git checkout -b feature/your-feature`)
2. Commit your changes (`git commit -am 'Add new feature'`)
3. Push to the branch (`git push origin feature/your-feature`)
4. Open a Pull Request

---

## License

This project is licensed under the MIT License.