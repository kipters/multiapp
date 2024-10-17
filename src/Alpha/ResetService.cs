
namespace Alpha;

internal sealed class ResetService(ILogger<ResetService> logger, IClusterClient cluster) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(30));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            var grain = cluster.GetGrain<ITickerController>(0);
            await grain.Reset();
            logger.LogInformation("Reset!");
        }
    }
}
