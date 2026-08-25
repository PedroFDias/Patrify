using Patrify.MessageBus.Messages;

namespace Patrify.Account.DTO
{
    public class TransactionCreatedEvent : BaseMessage
    {
        public Guid TransactionId { get; set; }
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
    }
}
