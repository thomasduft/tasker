using System.Threading.Tasks;

namespace tomware.Tasker.AspNetCoreEngine
{
  public class TaskExecution
  {
    private readonly ITaskDefinition definition;

    public TaskExecution(ITaskDefinition definition)
    {
      this.definition = definition;
    }

    public Task ExecuteAsync(TaskContext context = null)
    {
      if (context == null) context = new TaskContext();

      return this.definition.ExecuteAsync(context);
    }
  }
}
