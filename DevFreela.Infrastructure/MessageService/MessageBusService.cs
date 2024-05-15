using DevFreela.Core.Entities;
using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace DevFreela.Infrastructure.MessageService
{
    public class MessageBusService : IMessageBusService
    {
        private readonly ConnectionFactory _factory;
        public MessageBusService()
        {           
            _factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
        };
        }

        public async Task PublishService(string queue, byte[] message)
        {
            using (var connection = _factory.CreateConnection())
             
            using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: queue,
                        durable: false,
                        autoDelete: false,
                        exclusive: false,
                        arguments: null
                        );

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: message);
                }
            
        }
    }
}
