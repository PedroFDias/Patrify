namespace Patrify.MessageBus.Contracts
{
    public record BaseMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime MessageCreated { get; set; } = DateTime.UtcNow;
    }
}