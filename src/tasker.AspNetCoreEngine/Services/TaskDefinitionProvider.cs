using System;
using System.Collections.Generic;
using System.Linq;

namespace tomware.Tasker.AspNetCoreEngine
{
  public class TaskDefinitionProvider : ITaskDefinitionProvider
  {
    private readonly IEnumerable<ITaskDefinition> defintions;

    public TaskDefinitionProvider(IEnumerable<ITaskDefinition> defintions)
    {
      this.defintions = defintions;
    }

    public ITaskDefinition GetTaskDefinition(string type)
    {
      return this.defintions.First(t => t.Type == type);
    }

    public IEnumerable<ITaskDefinition> GetTaskDefinitions()
    {
      return this.defintions;
    }

    public void RegisterTaskDefinition(ITaskDefinition definition)
    {
      throw new NotImplementedException();
    }
  }
}
