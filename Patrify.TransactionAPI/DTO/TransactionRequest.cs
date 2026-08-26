using Microsoft.EntityFrameworkCore;
using Patrify.MessageBus.Contracts;
using Patrify.TransactionAPI.Entities;

namespace Patrify.TransactionAPI.DTO
{
    public record TransactionRequest(
        Guid AccountId,
        decimal Amount,
        string? Description,
        TransactionType Type
    ) : BaseMessage;
}