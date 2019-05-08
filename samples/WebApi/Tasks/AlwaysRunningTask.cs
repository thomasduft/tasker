using System;
using System.Threading.Tasks;
using tomware.Tasker.Core;

namespace WebApi.Tasks
{
  public class AlwaysRunningTask : ITaskDefinition
  {
    public string Type => "AlwaysRunningTask";

    public string ScheduleExpression => null;

    public async Task ExecuteAsync(TaskContext context)
    {
      Console.WriteLine($"{DateTime.Now}: Hello from AlwaysRunningTask...");

      await Task.CompletedTask;
    }
  }
}
