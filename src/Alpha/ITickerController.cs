using Shared.Alpha;

namespace Alpha;

[Alias("Alpha.ITickerController")]
internal interface ITickerController : ITickerGrain
{
    [Alias("Reset")]
    Task Reset();
}