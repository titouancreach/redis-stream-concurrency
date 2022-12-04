using Concurrency.Worker;
using StackExchange.Redis;
using Unitee.EventDriven.Abstraction;
using Unitee.EventDriven.DependencyInjection;
using Unitee.EventDriven.RedisStream;

IHost host = Host.CreateDefaultBuilder(args)
.ConfigureServices(services =>
{
    var cs = Environment.GetEnvironmentVariable("Redis__ConnectionString");
    var instance = Environment.GetCommandLineArgs().Last();
    ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(cs!);
    services.AddSingleton<IConnectionMultiplexer>(redis);
    services.AddScoped<IRedisStreamPublisher, RedisStreamPublisher>();
    services.AddScoped<IConsumer, Consumer>();
    Console.WriteLine("Starting worker " + instance);
    services.AddRedisStreamBackgroundReceiver("Concurrency", instance);
})
.Build();

host.Run();
