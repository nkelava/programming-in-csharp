# Programming in C#

Assignments from a university course on programming in C#. Each `class-XX` folder contains the work for one class, starting with console applications and moving on to desktop applications. Everything runs on Windows, Linux and macOS.

## Projects

| Class | Project | Type | Description |
|---|---|---|---|
| [class-01](class-01) | [NumberFormatting](class-01/NumberFormatting) | Console | Divides two numbers and prints the result in several standard numeric formats. |
| | [OverflowChecking](class-01/OverflowChecking) | Console | Converts a `long` to an `int` in a `checked` context to show how overflow is detected. |
| | [BankAccounts](class-01/BankAccounts) | Console | Menu for creating bank accounts with random account numbers and listing them. |
| [class-02](class-02) | [Shapes](class-02/Shapes) | Console | Inserts circles, squares and triangles and prints their total area, total perimeter and properties. Uses inheritance and abstract classes. |
| [class-03](class-03) | [ShapeEvents](class-03/ShapeEvents) | Console | The Shapes application extended with delegates and an event that is raised when a circle is added. |
| [class-04](class-04) | [MyBank](class-04/MyBank) | Desktop (Avalonia) | Form for creating bank accounts and searching for them by OIB (Croatian personal identification number). |
| [class-05](class-05) | [CurrencyConverter](class-05/CurrencyConverter) | Desktop (Avalonia) | Converts amounts between currencies using exchange rates from the Croatian National Bank (HNB) API. |
| [class-06](class-06) | [BankTransferSlip](class-06/BankTransferSlip) | Desktop (Avalonia) | Fills in a Croatian HUB-3A payment slip, validates the input and saves the transfer to a SQLite database. |
| [class-07](class-07) | [TicTacToe](class-07/TicTacToe) | Desktop (Avalonia) | Tic-tac-toe for two players or against the computer, with a leaderboard saved to a JSON file. |

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) on Windows, Linux or macOS
- Optionally an IDE such as Visual Studio 2026, JetBrains Rider or Visual Studio Code with the C# Dev Kit extension

The desktop applications use [Avalonia](https://avaloniaui.net/), a cross-platform UI framework similar to WPF. All NuGet packages are restored automatically on the first build.

## Running the projects

Run a project from the command line, for example:

```
dotnet run --project class-01/NumberFormatting
dotnet run --project class-07/TicTacToe
```

Or open the `.sln` file of a project in your IDE and start it. The `class-01/class-01.sln` solution contains all three class-01 projects.

## Notes

- **BankTransferSlip** and **TicTacToe** save their data in the user's local application data folder, which is created on first use:

  | Platform | Folder |
  |---|---|
  | Windows | `%LOCALAPPDATA%` |
  | Linux | `~/.local/share` |
  | macOS | `~/Library/Application Support` |

  BankTransferSlip uses `BankTransferSlip/BankTransferSlip.db`. TicTacToe uses `TicTacToe/leaderboard.json`, which starts with the players from [Leaderboard/leaderboard.json](class-07/TicTacToe/Leaderboard/leaderboard.json).
- **CurrencyConverter** uses version 2 of the HNB exchange rate API (`https://api.hnb.hr/tecajn/v2`), which may no longer be available since Croatia adopted the euro in 2023.
- A [GitHub Actions workflow](.github/workflows/build.yml) builds every project on Windows, Linux and macOS and runs the console applications.
