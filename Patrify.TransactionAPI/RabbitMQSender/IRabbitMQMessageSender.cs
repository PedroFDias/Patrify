using Patrify.MessageBus;

namespace Patrify.TransactionAPI.RabbitMQSender
{
    public interface IRabbitMQMessageSender
    {
        Task SendMessageAsync(BaseMessage message);
    }
}
