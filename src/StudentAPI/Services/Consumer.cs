using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace StudentAPI.Services
{
    public class Consumer : IConsumer
    {
        public dynamic getMessage()
        {
            var result = new Object();
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var chanel = connection.CreateModel();
            chanel.QueueDeclare("authen-request", durable: true, exclusive: false);
            var consumer = new EventingBasicConsumer(chanel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToString();
                var message = Encoding.UTF8.GetBytes(body);
                result = JsonSerializer.Serialize(message);
                Console.WriteLine("receive"+message);
            };
            chanel.BasicConsume("authen-request", true, consumer);
            return result;
        }
    }
}
