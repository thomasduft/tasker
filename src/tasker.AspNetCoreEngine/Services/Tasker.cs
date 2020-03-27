using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using tomware.Microcron.Core;

namespace tomware.Tasker.AspNetCoreEngine
{
  public class TaskItem
  {
    public string Type { get; set; }
    public string ScheduleExpression { get; set; }
    public DateTime? NextExecution { get; set; }
  }

  public class Tasker : BackgroundService
  {
    private readonly ILogger<Tasker> logger;
    private readonly TaskerConfiguration options;
    private readonly IServiceScopeFactory serviceScopeFactory;

    private IEnumerable<ITaskDefinition> taskDefinitions;
    private ConcurrentDictionary<string, TaskItem> taskItems;

    public Tasker(
      ILogger<Tasker> logger,
      IOptions<TaskerConfiguration> options,
      IServiceScopeFactory serviceScopeFactory
    )
    {
      this.logger = logger;
      this.options = options.Value;
      this.serviceScopeFactory = serviceScopeFactory;

      this.taskDefinitions = this.GetTaskDefinitions();
      this.taskItems = this.GetTaskItems();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        if (!this.options.Enabled) { continue; }

        this.logger.LogTrace("Looking for new tasks");

        var now = DateTime.Now;
        foreach (var item in this.taskItems.Values)
        {
          if (item.NextExecution <= now)
          {
            try
            {
              this.logger.LogTrace("Executing task {Task}", item.Type);

              // 1. Execute
              await this.ProcessItemAsync(item.Type);

              // 2. Update execution time
              item.NextExecution = this.GetNextExecutionDate(item.Type);
            }
            catch (Exception ex)
            {
              this.logger.LogError(ex, "Executing task {Task} failed!", item.Type);
            }
          }
        }

        await Task.Delay(this.options.Interval, stoppingToken);
      }
    }

    private ConcurrentDictionary<string, TaskItem> GetTaskItems()
    {
      var items = new ConcurrentDictionary<string, TaskItem>();

      foreach (var taskDefinition in this.taskDefinitions)
      {
        var item = new TaskItem
        {
          Type = taskDefinition.Type,
          ScheduleExpression = taskDefinition.ScheduleExpression,
          NextExecution = this.GetNextExecutionDate(taskDefinition.Type)
        };

        items.TryAdd(taskDefinition.Type, item);
      }

      return items;
    }

    private IEnumerable<ITaskDefinition> GetTaskDefinitions()
    {
      using (var scope = this.serviceScopeFactory.CreateScope())
      {
        IServiceProvider serviceProvider = scope.ServiceProvider;
        var taskDefinitionProvider = serviceProvider.GetRequiredService<ITaskDefinitionProvider>();

        return taskDefinitionProvider.GetTaskDefinitions();
      }
    }

    private DateTime? GetNextExecutionDate(string type)
    {
      var taskDefinition = this.taskDefinitions.First(t => t.Type == type);

      var now = DateTime.Now;
      return taskDefinition.ScheduleExpression != null
          ? new Cron(taskDefinition.ScheduleExpression).GetNextOccurrence(now)
          : now;
    }

    private async Task ProcessItemAsync(string type)
    {
      using (var scope = this.serviceScopeFactory.CreateScope())
      {
        IServiceProvider serviceProvider = scope.ServiceProvider;
        var taskDefinitionProvider = serviceProvider.GetRequiredService<ITaskDefinitionProvider>();

        var taskDefinition = taskDefinitionProvider.GetTaskDefinition(type);

        await taskDefinition.ExecuteAsync(null).ConfigureAwait(false);
      }
    }
  }
}
