# Realtime Docs Clone

A real-time collaborative document editor built with **C#** and **.NET**, inspired by Google Docs.

Quick status: Prototype — SignalR hub and a test client are implemented; the project lacks persistence, concurrency resolution (OT/CRDT), security, and automated tests.

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

## How to run locally (development)

1. Restore and build:

  ```bash
  dotnet restore
  dotnet build
  ```

2. Run the API (default dev profile uses HTTP on port 5010):

  ```bash
  dotnet run --project src/RealtimeDocsClone.Api
  ```

3. Open the test client:

  - Open `test-client.html` in a browser (file:// or serve it from a static server). Ensure the `HubConnection` URL matches the running API (default: `http://localhost:5010/hubs/document`).

4. Demo: open two browser windows, set the same Document ID, click **Join Document**, and type in the editor — updates should be broadcast between the windows.

## What this demonstrates to recruiters

- Ability to scaffold a modern .NET web app and use SignalR for real-time communication.
- Understanding of collaboration concerns (group routing, state sync, conflict resolution planning).
- Awareness of architectural layers and a clear plan to implement production features (CRDT/OT, persistence, tests, auth, scaling).

## Known gaps & prioritized fixes (summary)

- Concurrency control (CRDT or OT) — critical: needed to resolve concurrent edits and ensure state convergence.
- Persistence & snapshot/sync on join — high: new/returning clients need current document snapshots.
- Tests — high: add unit tests for CRDT logic and integration tests for SignalR flows.

## Suggested short roadmap

1. Implement a CRDT text model and unit tests.
2. Add persistence (EF Core + SQLite for local dev) and snapshot retrieval on `JoinDocument`.
3. Convert the client to send operations/patches instead of full documents; add debounce/batching.
4. Add integration tests for join→snapshot→update flow and setup CI.

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
