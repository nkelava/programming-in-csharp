# Programming in C#

Assignments from a university course on programming in C#. Each `class-XX` folder contains the work for one class, starting with console applications and moving on to WPF desktop applications.

## Projects

| Class | Project | Type | Description |
|---|---|---|---|
| [class-01](class-01) | [NumberFormatting](class-01/NumberFormatting) | Console, .NET Core 3.1 | Divides two numbers and prints the result in several standard numeric formats. |
| | [OverflowChecking](class-01/OverflowChecking) | Console, .NET Core 3.1 | Converts a `long` to an `int` in a `checked` context to show how overflow is detected. |
| | [BankAccounts](class-01/BankAccounts) | Console, .NET Core 3.1 | Menu for creating bank accounts with random account numbers and listing them. |
| [class-02](class-02) | [Shapes](class-02/Shapes) | Console, .NET 5 | Inserts circles, squares and triangles and prints their total area, total perimeter and properties. Uses inheritance and abstract classes. |
| [class-03](class-03) | [ShapeEvents](class-03/ShapeEvents) | Console, .NET Framework | The Shapes application extended with delegates and an event that is raised when a circle is added. |
| [class-04](class-04) | [MyBank](class-04/MyBank) | WPF, .NET Framework 4.7.2 | Form for creating bank accounts and searching for them by OIB (Croatian personal identification number). |
| [class-05](class-05) | [CurrencyConverter](class-05/CurrencyConverter) | WPF, .NET Framework 4.7.2 | Converts amounts between currencies using exchange rates from the Croatian National Bank (HNB) API. |
| [class-06](class-06) | [BankTransferSlip](class-06/BankTransferSlip) | WPF, .NET Framework 4.7.2 | Fills in a Croatian HUB-3A payment slip, validates the input and saves the transfer to a SQL Server LocalDB database. |
| [class-07](class-07) | [TicTacToe](class-07/TicTacToe) | WPF, .NET Framework 4.7.2 | Tic-tac-toe for two players or against the computer, with a leaderboard saved to a JSON file. |

## Requirements

- [Visual Studio](https://visualstudio.microsoft.com/) 2019 or newer with the **.NET desktop development** workload
- Windows for the WPF applications (class-04 to class-07) and the .NET Framework console application (class-03)

The class-01 and class-02 projects target .NET Core 3.1 and .NET 5, which are no longer supported. They still build with a newer .NET SDK. To run them without installing the old runtimes, set the `DOTNET_ROLL_FORWARD` environment variable to `Major`.

## Running the projects

Open the `.sln` file of a project in Visual Studio and start it. The `class-01/class-01.sln` solution contains all three class-01 projects.

The console applications can also be run from the command line, for example:

```
dotnet run --project class-01/NumberFormatting
```

## Notes

- **BankTransferSlip** needs a SQL Server LocalDB database file (`Database1.mdf`) that is not included in the repository. Its connection string in `App.config` points to a local path that has to be changed.
- **TicTacToe** reads and writes the leaderboard using an absolute path in `GameWindow.xaml.cs` and `LeaderboardWindow.xaml.cs`. Change it to the location of `Leaderboard/leaderboard.json` before running the game.
- **CurrencyConverter** uses version 2 of the HNB exchange rate API (`https://api.hnb.hr/tecajn/v2`), which may no longer be available since Croatia adopted the euro in 2023.
