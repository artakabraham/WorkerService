namespace WorkerService
{
    internal class MyService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"This is my Service running at {DateTime.Now}");
                await Task.Delay(2000, stoppingToken);
            }
        }
    }
}
