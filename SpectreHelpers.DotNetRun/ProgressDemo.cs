// This is a C# script that demonstrates the convenience use of SpectreHelpers when using Specture.Console Progress.
// Handlers for tasks are defined in-line, and will all be executed in parallel when `StartAsync` is called.
//
// With .NET 10, run with `dotnet run ProgressDemo.cs`
//
// Spectre.Console is included automatically as a transitive dependency.
// See https://spectreconsole.net for more information.

#:package 130Widgets.SpectreHelpers@[1.0.0, 2.0.0)

using Spectre.Console;

// Four different tasks are defined, and each will run in parallel while showing progress.
await AnsiConsole.Progress()
    .Columns(
    [
        new TaskDescriptionColumn(),
        new ProgressBarColumn(),
        new PercentageColumn(),
        new RemainingTimeColumn(),
        new SpinnerColumn()
    ])
    .AddTask("Task 1", async progressTask =>
    {
        // No looping here. Just a simple task that runs to completion.
        // When this task completes, the progress bar automatically be set to 100%.
        await Task.Delay(1000);
        progressTask.Increment(30);
        await Task.Delay(1000);
    })
    .AddTask("Task 2", async progressTask =>
    {
        // This task will loop until `IsFinished` is set to true. The default
        // maximum value of the progress bar is 100, so we'll increment by 10
        // each time, and the `ProgressTask` will return as finished when the
        // value reaches 100.
        while (!progressTask.IsFinished)
        {
            await Task.Delay(500);
            progressTask.Increment(10);
        }
    })
    // This task is fully implemented in a separate class. The name will be inferred from the method name.
    .AddTask(new MyHandlerClass().MyHandlerMethod3)
    .AddTask("Task 4", 100000, async progressTask =>
    {
        // This example is the same as Task 2, but shows how to the maximum value of the progress bar can be
        // any `double` value. This is convenient, as task incrementing does not have to be translated to percentages.
        while (!progressTask.IsFinished)
        {
            await Task.Delay(10);
            progressTask.Increment(300);
        }
    })
    .StartAsync();

class MyHandlerClass
{
    public async Task MyHandlerMethod3(ProgressTask task)
    {
        await Task.Delay(1000);
        task.Increment(30);
        await Task.Delay(1000);
    }
}
