namespace Spectre.Console;

public static class StatusExtensions
{
    public static async Task Status(this Task task, string message = "Working...", string? completeMessage = null)
    {
        try
        {
            await AnsiConsole.Status().StartAsync(message, async context =>
            {
                await task;
            });
        }
        finally
        {
            if (completeMessage is not null)
            {
                AnsiConsole.MarkupLine(completeMessage);
            }
        }
    }
}
