using System.Reflection;

namespace Spectre.Console;

public static class ProgressExtensions
{
    private static readonly object _lock = new();

    private static readonly Dictionary<Progress, List<(string taskName, double maxValue, Func<ProgressTask, Task> handler)>> _handlers = new();

    /// <summary>
    /// Adds a new task handler to the specified <see cref="Progress"/> instance. Use with <see cref="StartAsync(Progress)"/>.
    /// </summary>
    public static Progress AddTask(this Progress progress, string taskName, double maxValue, Func<ProgressTask, Task> handler)
    {
        lock (_lock)
        {
            if (!_handlers.TryGetValue(progress, out List<(string taskName, double maxValue, Func<ProgressTask, Task> handler)>? value))
            {
                _handlers.Add(progress, [(taskName, maxValue, handler)]);
            }
            else
            {
                value.Add((taskName, maxValue, handler));
            }
        }

        return progress;
    }

    /// <summary>
    /// Adds a new task handler to the specified <see cref="Progress"/> instance. Use with <see cref="StartAsync(Progress)"/>.
    /// </summary>
    public static Progress AddTask(this Progress progress, string taskName, Func<ProgressTask, Task> handler)
    {
        return progress.AddTask(taskName, 100, handler);
    }

    /// <summary>
    /// Adds a new task handler to the specified <see cref="Progress"/> instance. Use with <see cref="StartAsync(Progress)"/>.
    /// </summary>
    public static Progress AddTask(this Progress progress, Func<ProgressTask, Task> handler)
    {
        return progress.AddTask(handler.GetMethodInfo().Name, 100, handler);
    }

    /// <summary>
    /// Starts the specified <see cref="Progress"/> instance and all its tasks. Use with <see cref="AddTask(Progress, string, double, Func{ProgressTask, Task})"/>.
    /// </summary>
    public static async Task StartAsync(this Progress progress)
    {

        await progress.StartAsync(async ctx =>
        {
            if (_handlers.ContainsKey(progress))
            {
                var handlers = _handlers[progress];
                await Task.WhenAll(handlers.Select(async taskNameAndHandler =>
                {
                    var task = ctx.AddTask(taskNameAndHandler.taskName, new ProgressTaskSettings
                    {
                        AutoStart = true,
                        MaxValue = taskNameAndHandler.maxValue,
                    });

                    await taskNameAndHandler.handler(task);
                    if (!task.IsFinished)
                    {
                        task.Value(task.MaxValue);
                        task.StopTask();
                    }
                }));
            }
        });
    }
}