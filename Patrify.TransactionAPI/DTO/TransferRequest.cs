using Patrify.MessageBus.Contracts;
using Patrify.MessageBus.Contracts.Enums;

namespace Patrify.TransactionAPI.DTO
{
    public record TransferRequest(
        Guid AccountId,
        Guid targetAccountId,
        decimal Amount,
        string? Description,
        TransactionStatus Type
    ) : BaseMessage;
}