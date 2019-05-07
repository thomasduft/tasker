using System.Threading.Tasks;
using tomware.Microcron.Core;
using tomware.Tasker.Core;

namespace tomware.Tasker.Tests.Tasks
{
  public class TestTask : ITaskDefinition
  {
    public const string TYPE = "TestTask";

    public string Type => TYPE;

    public string ScheduleExpression => Expressions.DEFAULT_EXPRESSION;

    public string TestString { get; set; }

    public async Task ExecuteAsync(TaskContext context)
    {
      this.TestString = $"Hello from {TYPE}";

      if (context.ContainsKey(TestTaskVariable.KEY))
      {
        var variable = context.GetVariable<TestTaskVariable>(TestTaskVariable.KEY);

        this.TestString = variable.MyValue;
      }

      await Task.CompletedTask;
    }
  }

  public class TestTaskVariable : TaskVariableBase 
  {
    public const string KEY = "TestTaskContext";

    public string MyValue { get; set; }
  }
}
