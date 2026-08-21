using Microsoft.EntityFrameworkCore;
using Patrify.MessageBus;
using Patrify.TransactionAPI.Entities;

namespace Patrify.TransactionAPI.DTO
{
    public class TransactionRequest: BaseMessage
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public TransactionType Type { get; set; }
    }
}