using System.Threading.Tasks;
using MassTransit;
using MessageQueue;

namespace LoggerService
{
    public class LoggerRegisteredConsumer : IConsumer<ILogRegisteredEvent>
    {
        public async Task Consume(ConsumeContext<ILogRegisteredEvent> context)
        {
            //Send Logger To Elastic Search here
            //await Console.Out.WriteLineAsync($"Customer notification sent: Order id {context.Message.CorrelationId}");
        }
    }
}
