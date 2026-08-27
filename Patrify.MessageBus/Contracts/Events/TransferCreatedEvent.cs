using Patrify.MessageBus.Contracts.Enums;

namespace Patrify.MessageBus.Contracts.Events
{
    public record TransferCreatedEvent
    (
        Guid TransactionId,
        Guid AccountId,
        Guid TargetAccountId,
        decimal Amount,
        TransactionStatus Type
    ) : BaseMessage;
}
