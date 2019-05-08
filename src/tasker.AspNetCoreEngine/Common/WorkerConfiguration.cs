namespace tomware.Tasker.AspNetCoreEngine
{
  public class WorkerConfiguration
  {
    public bool Enabled { get; set; } = true;
    public int Interval { get; set; } = 5000;
  }
}
