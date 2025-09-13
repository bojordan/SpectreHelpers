# SpectreHelpers

[![NuGet](https://img.shields.io/nuget/v/130Widgets.SpectreHelpers.svg)](https://www.nuget.org/packages/130Widgets.SpectreHelpers/)
[![License](https://img.shields.io/github/license/bojordan/SpectreHelpers.svg)](https://github.com/bojordan/SpectreHelpers/blob/main/LICENSE)

A collection of helper extensions for [Spectre.Console](https://github.com/spectreconsole/spectre.console) that simplify common console application patterns, particularly progress tracking with multiple concurrent tasks.

## Features

- **Simplified Progress API**: Chain multiple progress tasks with a fluent interface
- **Automatic Task Management**: Tasks are automatically started and completed
- **Method Inference**: Automatically use method names as task descriptions
- **Concurrent Execution**: All tasks run concurrently for better performance
- **Built on Spectre.Console**: Leverages the full power of Spectre.Console's rich console capabilities

## Installation

Install via NuGet Package Manager:
```
dotnet add package 130Widgets.SpectreHelpers
```
