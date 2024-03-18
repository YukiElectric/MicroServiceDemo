using MassTransit;
using Newtonsoft.Json;
using SharedModel;

namespace AccountAPI.Common
{
    public class Consumer2 : IConsumer<MessageConsumer>
    {
        public async Task Consume(ConsumeContext<MessageConsumer> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"receive message 2: {jsonMessage}");
        }
    }
}
