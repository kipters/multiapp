using Alpha;
using Orleans.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddOrleans(_ => _
    .UseRedisClustering("redis")
    .ConfigureEndpoints(11111, 30000)
    .Configure<TypeManagementOptions>(o => o.TypeMapRefreshInterval = TimeSpan.FromSeconds(1))
    .Configure<ClusterOptions>(c => 
    {
        c.ClusterId = "multiapp";
        c.ServiceId = "multiapp";
    })
);

// builder.Services.AddHostedService<ResetService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
