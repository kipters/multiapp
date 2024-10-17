namespace Alpha;

internal sealed class TickerGrain : Grain, ITickerController
{
    internal int _value;

    public Task Reset()
    {
        _value = 0;
        return Task.CompletedTask;
    }

    public Task<int> Tick()
    {
        _value++;
        return Task.FromResult(_value);
    }
}
