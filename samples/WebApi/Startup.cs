using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using tomware.Tasker.AspNetCoreEngine;
using WebApi.Tasks;

namespace WebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var workerConfiguration = this.GetTaskerConfiguration(services);
      services.AddTaskerEngineServices(workerConfiguration.Value);

      // Tasks
      services.AddTransient<ITaskDefinition, AlwaysRunningTask>();
      services.AddTransient<ITaskDefinition, MinuteRunningTask>();
      services.AddTransient<ITaskDefinition, TenSecondsRunningTask>();

      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      
      app.UseRouting();
      
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }

    private IOptions<TaskerConfiguration> GetTaskerConfiguration(IServiceCollection services)
    {
      var tasker = this.Configuration.GetSection("Tasker");
      services.Configure<TaskerConfiguration>(tasker);

      return services
      .BuildServiceProvider()
      .GetRequiredService<IOptions<TaskerConfiguration>>();
    }
  }
}
