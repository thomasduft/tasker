using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using tomware.Tasker.Core;

namespace tomware.Tasker.AspNetCoreEngine
{
  public static class EngineServiceExtensions
  {
    public static IServiceCollection AddTaskerEngineServices(
      this IServiceCollection services,
      WorkerConfiguration workerConfiguration = null
    )
    {
      workerConfiguration = workerConfiguration ?? new WorkerConfiguration();
      services.Configure<WorkerConfiguration>(config =>
      {
        config.Enabled = workerConfiguration.Enabled;
        config.Interval = workerConfiguration.Interval;
      });

      services.AddSingleton<IHostedService, Worker>();

      services.AddScoped<ITaskDefinitionProvider, TaskDefinitionProvider>();

      return services;
    }
  }
}
