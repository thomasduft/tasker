using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace tomware.Tasker.AspNetCoreEngine
{
  public static class EngineServiceExtensions
  {
    public static IServiceCollection AddTaskerEngineServices(
      this IServiceCollection services,
      TaskerConfiguration taskerConfiguration = null
    )
    {
      taskerConfiguration = taskerConfiguration ?? new TaskerConfiguration();
      services.Configure<TaskerConfiguration>(opt =>
      {
        opt.Enabled = taskerConfiguration.Enabled;
        opt.Interval = taskerConfiguration.Interval;
      });

      services.AddSingleton<IHostedService, Tasker>();

      services.AddScoped<ITaskDefinitionProvider, TaskDefinitionProvider>();

      return services;
    }
  }
}
