using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using tomware.Tasker.Core;
using tomware.Tasker.Tests.Tasks;

namespace tomware.Tasker.Tests.Core
{
  [TestClass]
  public class TaskExecutionTest
  {
    [TestMethod]
    public async Task ExecuteAsync_NoContextGiven_ExecutesOnce()
    {
      // Arrange
      var task = new TestTask();
      var execution = new TaskExecution(task);

      // Act
      await execution.ExecuteAsync();

      // Assert
      Assert.AreEqual($"Hello from {TestTask.TYPE}", task.TestString);
    }

    [TestMethod]
    public async Task ExecuteAsync_ContextGiven_ExecutesOnce()
    {
      // Arrange
      var task = new TestTask();
      var execution = new TaskExecution(task);
      var context = new TaskContext()
        .AddVariable(TestTaskVariable.KEY, new TestTaskVariable
        {
          MyValue = "MyValue"
        });

      // Act
      await execution.ExecuteAsync(context);

      // Assert
      Assert.AreEqual("MyValue", task.TestString);
    }
  }
}
