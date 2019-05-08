using System.Threading.Tasks;

namespace tomware.Tasker.Core
{
  public interface ITaskDefinition
  {
    string Type { get; }

    string ScheduleExpression { get; }

    Task ExecuteAsync(TaskContext context);
  }
}
