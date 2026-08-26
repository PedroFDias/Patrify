namespace Patrify.MessageBus.RabbitMQ.Publish
{
    public interface IRabbitMQPublish
    {
        Task Publish<T>(T message, string exchange, string exchangeType, string routingKey);
    }
}
