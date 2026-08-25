using Patrify.MessageBus.Messages;
using RabbitMQ.Client;

namespace Patrify.MessageBus.RabbitMQ.Publish
{
    public interface IRabbitMQPublish
    {
        Task Publish<T>(T message, string exchange, string exchangeType, string routingKey);
    }
}
