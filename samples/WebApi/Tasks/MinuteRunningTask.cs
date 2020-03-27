using System;
using System.Threading.Tasks;
using tomware.Microcron.Core;
using tomware.Tasker.AspNetCoreEngine;

namespace WebApi.Tasks
{
  public class MinuteRunningTask : ITaskDefinition
  {
    public string Type => nameof(MinuteRunningTask);

    public string ScheduleExpression => Expressions.DEFAULT_EXPRESSION;

    public async Task ExecuteAsync(TaskContext context)
    {
      Console.WriteLine($"{DateTime.Now}: Hello from {this.Type}...");

      await Task.CompletedTask;
    }
  }
}
