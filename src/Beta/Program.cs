using Orleans.Configuration;
using Shared.Alpha;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddOrleans(_ => _
    .UseRedisClustering("redis")
    .ConfigureEndpoints(11112, 30001)
    .UseDashboard(x => x.HostSelf = false)
    .Configure<TypeManagementOptions>(o => o.TypeMapRefreshInterval = TimeSpan.FromSeconds(1))
    .Configure<ClusterOptions>(c => 
    {
        c.ClusterId = "multiapp";
        c.ServiceId = "multiapp";
    })
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Map("/dashboard", _ => _.UseOrleansDashboard());
app.MapGet("/env", () => "Hello!");

app.Map("/tick/{id:int}", async (int id, IClusterClient cluster) => 
{
    var grain = cluster.GetGrain<ITickerGrain>(id);
    return await grain.Tick();
});

app.UseHttpsRedirection();

app.Run();
