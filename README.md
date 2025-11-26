# Realtime Docs Clone

A real-time collaborative document editor built with **C#** and **.NET**, inspired by Google Docs.

The goal of this project is to explore:

- Real-time communication with **WebSockets / SignalR**
- **Concurrency control** for collaborative editing
- **Non-blocking, scalable** server design
- Clean **architecture** and professional repository structure

---

## Tech Stack

- **Backend:** .NET 8, ASP.NET Core, SignalR
- **Language:** C#
- **Database (planned):** PostgreSQL (or SQLite for local dev)
- **Testing:** xUnit
- **Client:** Web frontend (initially simple HTML/JS, later possibly React)

---

## High-Level Architecture (Planned)

**Client (Browser)**  
↕ (WebSockets / SignalR)  
**API / Gateway Layer**  
↕  
**Collaboration / Application Services**  
↕  
**Domain Layer** (Document, Operations, Sessions)  
↕  
**Infrastructure Layer** (Database, caching, SignalR backplane, etc.)

Key architectural concerns:

- Managing concurrent edits from multiple users
- Avoiding blocking I/O and long-running operations on request threads
- Keeping the system responsive under load

---

## Repository Structure

```text
realtime-docs-clone/
  src/
    RealtimeDocsClone.Api/           # ASP.NET Core API + SignalR hubs
    RealtimeDocsClone.Application/   # (planned) Use cases, services
    RealtimeDocsClone.Domain/        # (planned) Domain entities, logic
    RealtimeDocsClone.Infrastructure/# (planned) EF Core, DB, external integrations
  tests/
    RealtimeDocsClone.Tests/         # xUnit test project
  RealtimeDocsClone.sln              # Solution file
  .gitignore
  LICENSE
  README.md
