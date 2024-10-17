
using Shared.Alpha;

namespace Beta;

internal sealed class TickerService(ILogger<TickerService> logger, IClusterClient clusterClient) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            var grain = clusterClient.GetGrain<ITickerGrain>(0);
            var value = await grain.Tick();
            logger.LogInformation("Tick: {value}", value);
        }
    }
}
