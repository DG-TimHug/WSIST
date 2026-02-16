# WSIST — What Should I Study Today

WSIST (*What Should I Study Today*) is a Blazor‑based study planner designed to help students organize tests, track understanding, and decide what to study next based on urgency and preparedness.

This project was built as part of my software engineering learning journey and focuses heavily on clean architecture, separation of concerns, and maintainable state management.

---

# Overview

WSIST helps students:

- Track upcoming tests
- Monitor their level of understanding per test
- Estimate workload and urgency
- Organize study priorities
- Persist data locally in a structured format

The system is designed with a strong separation between:

- UI layer (Blazor Web App)
- Business logic layer (WSIST.Engine)
- Persistence layer (JSON‑based storage)

This makes the project easy to extend later with databases, APIs, or additional frontends.

---

# Features

## Current

- Create tests with:
  - Title
  - Subject
  - Due date
  - Volume (workload estimate)
  - Understanding level

- Edit existing tests
- Delete tests
- Persistent storage using JSON
- Clean state management via dependency injection
- Modal‑based editing and creation UI
- Fully testable business logic

## Planned

- Priority calculation algorithm
- "What should I study today" recommendation engine
- PostgreSQL / database support
- User accounts
- Cloud sync
- Study history tracking
- Analytics dashboard

---

# Architecture

The project follows a clean layered architecture:

```
WSIST
│
├── WSIST.Web        → Blazor UI
├── WSIST.Engine     → Business logic
└── WSIST.Tests      → NUnit test project
```

## Responsibilities

### WSIST.Web

Handles:

- UI rendering
- User interaction
- Modal state
- Calling Engine services

Contains no business logic.


### WSIST.Engine

Core system logic:

- TestManagement service
- Load‑modify‑save persistence pattern
- JSON serialization
- Domain models

Fully independent of UI.


### WSIST.Tests

Contains unit tests for:

- Creating tests
- Editing tests
- Deleting tests
- Persistence correctness

Uses NUnit.

---

# Technology Stack

## Frontend

- Blazor Server
- Razor Components
- InteractiveServer render mode

## Backend Logic

- C# .NET
- Dependency Injection
- Clean Architecture principles

## Persistence

- JSON file storage

## Testing

- NUnit

---

# Design Principles

Key architectural goals:

- Separation of concerns
- Testable business logic
- UI independent from data layer
- Clean state management
- Explicit load → modify → save flow

Avoids:

- Hidden state
- Tight UI‑logic coupling
- Database dependency

---

# Example Workflow

User creates a test:

```
UI → TestManagement.AddTest()
   → Engine loads JSON
   → modifies data
   → saves JSON
   → UI refreshes state
```

---

# Project Structure

```
WSIST/
│
├── WSIST.Web/
│   ├── Pages/
│   ├── Components/
│   └── Program.cs
│
├── WSIST.Engine/
│   ├── Models/
│   ├── Services/
│   └── Persistence/
│
├── WSIST.Tests/
│   └── TestManagementTests.cs
│
└── README.md
```

---

# Getting Started

## Requirements

- .NET 9 SDK
- Visual Studio or Rider

## Run the project

```
dotnet build

dotnet run --project WSIST.Web
```

Then open:

```
https://localhost:xxxx
```

---

# Testing

Run tests:

```
dotnet test
```

---

# Future Vision

WSIST is intended to evolve into a fully featured study planning system with:

- Intelligent recommendations
- Multi‑device sync
- Web deployment
- Database backend

---

# Lessons Learned

Major learning areas from this project:

- Blazor component state management
- Dependency injection patterns
- Clean architecture design
- JSON persistence
- Unit testing best practices
- Separation of UI and business logic

---

# Author

Tim Hug

---

# Status

Actively developed

