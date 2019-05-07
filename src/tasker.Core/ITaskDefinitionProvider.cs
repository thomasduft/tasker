using System.Collections.Generic;

namespace tomware.Tasker.Core
{
  public interface ITaskDefinitionProvider
  {
    /// <summary>
    /// Registers a task definition.
    /// </summary>
    /// <param name="definition"></param>
    void RegisterTaskDefinition(ITaskDefinition definition);

    /// <summary>
    /// Returns a task definition.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    ITaskDefinition GetTaskDefinition(string type);

    /// <summary>
    /// Returns a list of task definitions.
    /// </summary>
    /// <returns></returns>
    IEnumerable<ITaskDefinition> GetTaskDefinitions();
  }
}
