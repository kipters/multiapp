using Orleans;

namespace Shared.Alpha;

[Alias("Shared.Alpha.ITickerGrain")]
public interface ITickerGrain : IGrainWithIntegerKey
{
    [Alias("Tick")]
    Task<int> Tick();
}