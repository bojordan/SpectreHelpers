# SpectreHelpers

[![NuGet](https://img.shields.io/nuget/v/130Widgets.SpectreHelpers.svg)](https://www.nuget.org/packages/130Widgets.SpectreHelpers/)
[![License](https://img.shields.io/github/license/bojordan/SpectreHelpers.svg)](https://github.com/bojordan/SpectreHelpers/blob/main/LICENSE.txt)

A collection of helper extensions for [Spectre.Console](https://github.com/spectreconsole/spectre.console) that simplify common console application patterns.

The helpers currently address progress tracking with multiple concurrent tasks.

## Installation

Install via NuGet Package Manager:
```
dotnet add package 130Widgets.SpectreHelpers
```

Include when using `dotnet run app.cs`:
```
#:package 130Widgets.SpectreHelpers@[1.0.0, 2.0.0)
```

## Example

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