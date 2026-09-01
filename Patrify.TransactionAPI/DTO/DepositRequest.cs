using Patrify.MessageBus.Contracts;

namespace Patrify.TransactionAPI.DTO
{
    public record DepositRequest(
        string Cpf,
        decimal Amount,
        string? Description,
        TransactionType Type
    ) : BaseMessage;
}