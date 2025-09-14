using Spectre.Console;

AnsiConsole.MarkupLine("[bold red]Hello[/] [green]World[/]");

var intArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

await AnsiConsole.Status()
    .Spinner(Spinner.Known.Dots)
    .SpinnerStyle(Style.Parse("yellow"))
    .StartAsync("Doing rando things while a spinner spins...", async ctx =>
    {
        foreach (var s in intArray)
        {
            AnsiConsole.MarkupLine($"[bold green]Super-awesome[/] [yellow]operation[/] [blue]{s}[/]");
            // Simulate some work
            await Task.Delay(25);
        }
    });