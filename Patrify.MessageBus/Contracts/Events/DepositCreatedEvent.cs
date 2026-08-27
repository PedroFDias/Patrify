using Patrify.MessageBus.Contracts.Enums;

namespace Patrify.MessageBus.Contracts.Events
{
    public record DepositCreatedEvent
    (
        Guid TransactionId,
        Guid AccountId,
        decimal Amount,
        TransactionStatus Type
    ) : BaseMessage;
}
