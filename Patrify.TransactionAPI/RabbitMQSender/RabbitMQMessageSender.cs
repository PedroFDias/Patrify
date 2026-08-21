using Patrify.MessageBus;
using Patrify.TransactionAPI.DTO;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Patrify.TransactionAPI.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;
        private const string ExchangeName = "transactionExchange";

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }
        public async Task SendMessageAsync(BaseMessage message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password
            };

            _connection = await factory.CreateConnectionAsync();

            using var channel = await _connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: ExchangeName, ExchangeType.Fanout, false);

            byte[] body = GetMessageAsByteArray(message);

            await channel.BasicPublishAsync(
                exchange: ExchangeName, "", mandatory:false ,basicProperties: new BasicProperties(), body: body);
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize<TransactionRequest>((TransactionRequest)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
