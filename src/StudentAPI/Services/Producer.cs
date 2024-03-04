using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace StudentAPI.Services
{
    public class Producer : IProducer
    {
        public void sendMessage<T>(T message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var chanel = connection.CreateModel();
            chanel.QueueDeclare("authen-request", durable: true, exclusive: false);
            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);
            chanel.BasicPublish("", "authen-request", body: body);
        }
    }
}
