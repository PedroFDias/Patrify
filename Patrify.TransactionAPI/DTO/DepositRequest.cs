using Patrify.MessageBus.Contracts;

namespace Patrify.TransactionAPI.DTO
{
    public record DepositRequest(
        Guid AccountId,
        decimal Amount,
        string? Description,
        TransactionType Type
    ) : BaseMessage;
}