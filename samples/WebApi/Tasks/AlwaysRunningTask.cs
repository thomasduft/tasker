using System;
using System.Threading.Tasks;
using tomware.Tasker.AspNetCoreEngine;

namespace WebApi.Tasks
{
  public class AlwaysRunningTask : ITaskDefinition
  {
    public string Type => nameof(AlwaysRunningTask);

    public string ScheduleExpression => null;

    public async Task ExecuteAsync(TaskContext context)
    {
      Console.WriteLine($"{DateTime.Now}: Hello from ${this.Type}...");

      await Task.CompletedTask;
    }
  }
}
