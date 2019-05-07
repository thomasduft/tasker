using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Engine.Common;

namespace WebApi.Engine.Services
{
  public class Worker : BackgroundService
  {
    private readonly ILogger<Worker> logger;
    private readonly WorkerConfiguration options;

    public Worker(ILogger<Worker> logger, IOptions<WorkerConfiguration> options)
    {
      this.logger = logger;
      this.options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        this.logger.LogTrace("I am working...");


        await Task.Delay(this.options.Interval, stoppingToken);
      }
    }

    //public override async Task StopAsync(CancellationToken stoppingToken)
    //{
    //  this.logger.LogTrace($"Stopping worker");
      
    //}
  }
}
