using Patrify.MessageBus.Contracts.Enums;

namespace Patrify.MessageBus.Contracts.Events
{
    public record TransactionStatusChangeEvent(
         Guid TransactionId,
         TransactionStatus Status
    ) { }
}
