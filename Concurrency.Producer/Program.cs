
using System.Diagnostics;
using Concurrency.Common;
using StackExchange.Redis;
using Unitee.EventDriven.RedisStream;

var eventPerSecond = 50;

var running = true;

Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
{
    e.Cancel = true;
    running = false;
};

var cs = Environment.GetEnvironmentVariable("Redis__ConnectionString");
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(cs!);

var publisher = new RedisStreamPublisher(redis);

var count = 0;
await publisher.PublishAsync(new StartEvt());

while (running)
{
    var sw = Stopwatch.StartNew();
    await publisher.PublishAsync(new Evt(count));
    count++;
    sw.Stop();
    var toWait = TimeSpan.FromSeconds(1.0 / eventPerSecond) - sw.Elapsed;

    Console.WriteLine(toWait);

    await Task.Delay(Math.Max((int)toWait.TotalMilliseconds, 0));
}

await publisher.PublishAsync(new StopEvt());

Console.WriteLine("Stopping with {0} events sent", count);
