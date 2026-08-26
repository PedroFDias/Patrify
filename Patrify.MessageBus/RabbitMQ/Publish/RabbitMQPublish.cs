using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Patrify.MessageBus.RabbitMQ.Publish
{
    public class RabbitMQPublish : IRabbitMQPublish
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;

        public RabbitMQPublish()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }
        public async Task Publish<T>(T message, string exchange, string exchangeType, string routingKey)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };

            await using var connection =
                await factory.CreateConnectionAsync();

            await using var channel =
                await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(
                exchange: exchange,
                type: exchangeType,
                durable: true,
                autoDelete: false
            );

            var body = GetMessageAsByteArray(message);

            await channel.BasicPublishAsync(
                exchange: exchange,
                routingKey: routingKey,
                body: body
            );
        }

        private byte[] GetMessageAsByteArray<T>(T message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(message, options);

            return Encoding.UTF8.GetBytes(json);
        }
    }
}
