using System;
using System.Collections.Generic;

namespace tomware.Tasker.Core
{
  public class TaskContext
  {
    private Dictionary<string, TaskVariableBase> Variables { get; set; }

    /// <summary>
    /// Indicates whether variables are available.
    /// </summary>
    public bool HasVariables
    {
      get { return this.Variables.Count > 0; }
    }

    public TaskContext(Dictionary<string, TaskVariableBase> variables = null)
    {
      this.Variables = new Dictionary<string, TaskVariableBase>();
    }

    public TaskContext AddVariable(string key, TaskVariableBase value)
    {
      if (this.Variables.ContainsKey(key))
      {
        throw new InvalidOperationException($"Key {key} exists already!");
      }

      this.Variables.Add(key, value);

      return this;
    }

    /// <summary>
    /// Checks whether a key is available for the variables dictionary.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ContainsKey(string key)
    {
      if (!this.HasVariables) return false;

      return this.Variables.ContainsKey(key);
    }

    /// <summary>
    /// Gets a workflow variable by its key.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T GetVariable<T>(string key) where T : TaskVariableBase
    {
      if (!this.ContainsKey(key))
        throw new Exception(string.Format("Key '{0}' not found!", key));

      return (T)this.Variables[key];
    }
  }
}