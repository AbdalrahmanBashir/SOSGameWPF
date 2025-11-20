# SOS Game – C#-Centric Overview

## Overview
- WPF desktop implementation of the classic SOS pencil-and-paper game.
- Logic packaged in `SOSGameLogic` class library, GUI in `SOSGameGU`, tests in `SOSGameLogicTest`.
- Emphasis on object-oriented and idiomatic C# patterns (interfaces, inheritance, event-driven UI, async flows).

## Solution Layout
- `SOSGame.sln` – ties the UI, logic library, and test project.
- `SOSGameLogic` – reusable rules engine (no UI dependencies).
- `SOSGameGU` – WPF presentation layer built around `MainWindow` and manager classes.
- `SOSGameLogicTest` – xUnit tests targeting the logic assembly.

## Highlighted C# Concepts
- **Interfaces for contracts**: `IGame`, `IBoard`, `IPlayer`, and `IBaseGameMode` decouple implementations and enable polymorphism.
- **Abstract base classes**: `BasePlayer` and `BaseGameMode` capture shared behavior while requiring concrete overrides (`HumanPlayer`, `ComputerPlayer`, `SimpleGameMode`, `GeneralGameMode`).
- **Encapsulation & immutability**: `Game` hides board state, exposes read-only collections (`List<SOSLine>`) for rendering.
- **Enums & DTOs**: `SOSLineType` plus the immutable `SOSLine` model describe SOS detections with strongly typed metadata.
- **Generics & tuples**: `List<SOSLine>` for collections, `Tuple<int,int>` for board coordinates, reinforcing type safety.
- **Event-driven UI**: WPF `Button.Click`, `RadioButton.Checked`, and `DispatcherTimer.Tick` handlers in `MainWindow` showcase delegates and event subscriptions.
- **Async/await**: `Cell_Click` and `HandleAutomaticMoves` leverage `Task`/`await` to keep the UI responsive while simulating delays.
- **Dependency-style composition**: Manager classes (`GameSetupManager`, `PlayerSelectionManager`, `ActiveGameManager`, `DrawSOSLineManager`) receive collaborators through constructors instead of hard dependencies, making the codebase easier to extend and test.
- **Unit testing**: xUnit `Fact`/`Theory` attributes validate board behavior, demonstrating how to isolate logic from UI concerns.

## Game Flow in C#
1. **Setup** – `MainWindow` wires event handlers, instantiates managers, and constructs a `Game` with chosen `IBaseGameMode`.
2. **Player configuration** – `PlayerSelectionManager` reacts to radio buttons, sets symbols through `IPlayer.SetPlayerSymbol`, and swaps concrete player types (`HumanPlayer` vs `ComputerPlayer`).
3. **Move execution** – `ActiveGameManager` orchestrates clicks, validates via `IGame.IsCellOccupied`, and delegates to `IGame.MakeMove`.
4. **Rule evaluation** – `Game` relies on `BaseGameMode.DetectSOSLine` helpers (horizontal, vertical, diagonal checks) to create `SOSLine` records and update scores.
5. **Rendering** – `DrawSOSLineManager` converts each `SOSLine` into a WPF `Line` shape; player colors derive from object identity (`player1` vs `player2`).
6. **Turn management** – `IGame.SwitchPlayer` plus dispatcher updates keep UI labels (`txtCurrentPlayerTurn`) accurate.

## Extending the Logic
- Implement a new `IBaseGameMode` derivative to change win conditions without touching UI code.
- Create additional `IPlayer` types (e.g., heuristic AI) by inheriting from `BasePlayer` and overriding `MakeMove`.
- Because managers work with interfaces, you can inject mocked counterparts for integration testing or CLI hosts.

## Tests
- Run `dotnet test SOSGame.sln` (or from solution root) to execute `SOSGameLogicTest`.
- Existing suites cover `Board`, `Game`, `ComputerPlayer`, and mode logic; add new tests alongside new rules for faster feedback.

## Building and Running
1. Install Visual Studio (or vs-build-tools) with `.NET Desktop Development`.
2. Open `SOSGame.sln`, set `SOSGameGU` as startup project, press **F5**.
3. For CLI build: `dotnet build SOSGame.sln`.

## Contributing Tips
- Keep UI-free logic inside `SOSGameLogic` to preserve testability.
- Favor interface-first designs when introducing new managers or services.
- Leverage WPF data binding (e.g., `INotifyPropertyChanged`) if you expand UI state—current code uses direct control updates for clarity.

Enjoy exploring the C# patterns embedded in this SOS implementation!