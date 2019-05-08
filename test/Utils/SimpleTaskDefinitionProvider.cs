using System.Collections.Generic;
using System.Linq;
using tomware.Tasker.AspNetCoreEngine;

namespace tomware.Tasker.Tests.Utils
{
  public class SimpleTaskDefinitionProvider : ITaskDefinitionProvider
  {
    private List<ITaskDefinition> definitions = null;

    private static SimpleTaskDefinitionProvider instance;

    public static SimpleTaskDefinitionProvider Instance
    {
      get
      {
        if (instance == null) instance = new SimpleTaskDefinitionProvider();

        return instance;
      }
    }

    private SimpleTaskDefinitionProvider()
    {
      this.definitions = new List<ITaskDefinition>();
    }

    internal void Invalidate()
    {
      this.definitions = new List<ITaskDefinition>();
    }

    public ITaskDefinition GetTaskDefinition(string type)
    {
      return this.definitions.First(w => w.Type == type);
    }

    public IEnumerable<ITaskDefinition> GetTaskDefinitions()
    {
      return this.definitions;
    }

    public void RegisterTaskDefinition(ITaskDefinition definition)
    {
      this.definitions.Add(definition);
    }
  }
}
