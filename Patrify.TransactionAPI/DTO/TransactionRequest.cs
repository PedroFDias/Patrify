using Microsoft.EntityFrameworkCore;
using Patrify.MessageBus.Messages;
using Patrify.TransactionAPI.Entities;

namespace Patrify.TransactionAPI.DTO
{
    public class TransactionRequest: BaseMessage
    {
        public Guid AccountID { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public TransactionType Type { get; set; }
    }
}