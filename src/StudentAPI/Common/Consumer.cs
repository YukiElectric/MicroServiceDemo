using MassTransit;
using Newtonsoft.Json;
using SharedModel;

namespace StudentAPI.Common
{
    public class Consumer : IConsumer<AuthenRespone>
    {
        public async Task Consume(ConsumeContext<AuthenRespone> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"receive message: {jsonMessage}");
        }
    }
}
