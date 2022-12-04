using Concurrency.Common;
using Unitee.EventDriven.RedisStream;

namespace Concurrency.Worker;

public class Consumer : IRedisStreamConsumer<Common.Evt>
{
    public async Task ConsumeAsync(Evt message)
    {
        Console.WriteLine("RECEIVED: " + message);
        await Task.Delay(1000);
    }
}