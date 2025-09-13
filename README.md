# SpectreHelpers

[![NuGet](https://img.shields.io/nuget/v/130Widgets.SpectreHelpers.svg)](https://www.nuget.org/packages/130Widgets.SpectreHelpers/)

[![License](https://img.shields.io/github/license/bojordan/SpectreHelpers.svg)](https://github.com/bojordan/SpectreHelpers/blob/main/LICENSE.txt)

**SpectreHelpers** is a collection of helper extensions for [Spectre.Console](https://github.com/spectreconsole/spectre.console) that simplify common console application patterns, especially progress tracking with multiple concurrent tasks.

---

## Installation

Install via NuGet:

```sh
dotnet add package 130Widgets.SpectreHelpers
```

For C# scripts (`dotnet run app.cs`):

```csharp
#:package 130Widgets.SpectreHelpers@[1.0.0, 2.0.0)
```

---

## Usage

The helpers make it easy to add and run multiple progress tasks in parallel:

```csharp
using Spectre.Console;

await AnsiConsole.Progress()
    .AddTask("Task 1", async progressTask =>
    {
        progressTask.Increment(10);
        await Task.Delay(500);
    })
    .AddTask("Task 2", async progressTask =>
    {
        while (!progressTask.IsFinished)
        {
            await Task.Delay(250);
            progressTask.Increment(10);
        }
    })
    .StartAsync();
```

---

## Examples

- [C# script example (`dotnet run app.cs`)](https://github.com/bojordan/SpectreHelpers/tree/main/SpectreHelpers.DotNetRun)
- [Console app example](https://github.com/bojordan/SpectreHelpers/tree/main/SpectreHelpers.ProgressDemo)

---

## License

This project is licensed under the MIT License. See the [LICENSE.txt](LICENSE.txt) file for details.