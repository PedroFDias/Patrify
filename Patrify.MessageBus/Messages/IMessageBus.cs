namespace Patrify.MessageBus.Messages
{
    public interface IMessageBus
    {
        Task PublicMessage(BaseMessage message, string queueName);
    }
}
