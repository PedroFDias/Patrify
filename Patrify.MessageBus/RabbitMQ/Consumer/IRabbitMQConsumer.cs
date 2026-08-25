namespace Patrify.MessageBus.RabbitMQ.Consumer
{
    public interface IRabbitMQConsumer
    {
        Task ConsumeAsync<T>(
            string exchange,
            string exchangeType,
            string queue,
            string routingKey,
            Func<T, Task> handler
        );
    }
}
