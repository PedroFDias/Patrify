using Patrify.MessageBus.Contracts.Enums;

namespace Patrify.MessageBus.Contracts.Events
{
    public record TransactionCreatedEvent
    (
        Guid TransactionId,
        Guid AccountId,
        decimal Amount,
        TransactionType Type
    ) : BaseMessage;
}
