using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using tomware.Tasker.AspNetCoreEngine;
using tomware.Tasker.Core;
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
      var workerConfiguration = this.GetWorkerConfiguration(services);
      services.AddTaskerEngineServices(workerConfiguration.Value);

      // Tasks
      services.AddTransient<ITaskDefinition, AlwaysRunningTask>();
      services.AddTransient<ITaskDefinition, MinuteRunningTask>();

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }

    private IOptions<WorkerConfiguration> GetWorkerConfiguration(IServiceCollection services)
    {
      var worker = this.Configuration.GetSection("Worker");
      services.Configure<WorkerConfiguration>(worker);

      return services
      .BuildServiceProvider()
      .GetRequiredService<IOptions<WorkerConfiguration>>();
    }
  }
}
