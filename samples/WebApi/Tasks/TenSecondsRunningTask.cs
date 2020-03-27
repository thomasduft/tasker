using System;
using System.Threading.Tasks;
using tomware.Tasker.AspNetCoreEngine;

namespace WebApi.Tasks
{
  public class TenSecondsRunningTask : ITaskDefinition
  {
    public string Type => nameof(TenSecondsRunningTask);

    public string ScheduleExpression => "0,10,20,30,40,50 * * * * *";

    public async Task ExecuteAsync(TaskContext context)
    {
      Console.WriteLine($"{DateTime.Now}: Hello from {this.Type}...");

      await Task.CompletedTask;
    }
  }
}
