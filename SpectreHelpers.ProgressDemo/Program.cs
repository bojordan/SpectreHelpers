using Spectre.Console;

await AnsiConsole.Progress()
    .Columns(new ProgressColumn[]
    {
        new TaskDescriptionColumn(),
        new ProgressBarColumn(),
        new PercentageColumn(),
        new RemainingTimeColumn(),
        new SpinnerColumn()
    })
    .AddTask("Task 1", async progressTask =>
    {
        await Task.Delay(1000);
        progressTask.Increment(30);
        await Task.Delay(1000);
    })
    .AddTask("Task 2", async progressTask =>
    {
        while (!progressTask.IsFinished)
        {
            await Task.Delay(500);
            progressTask.Increment(10);
        }
    })
    .AddTask(MyHandler.MyHandlerMethod)
    .AddTask("Task 42", 100000, async progressTask =>
    {
        while (!progressTask.IsFinished)
        {
            await Task.Delay(10);
            progressTask.Increment(300);
        }
    })
    .StartAsync();

public static class MyHandler
{
    public static async Task MyHandlerMethod(ProgressTask task)
    {
        await Task.Delay(1000);
        task.Increment(30);
        await Task.Delay(1000);
    }
}