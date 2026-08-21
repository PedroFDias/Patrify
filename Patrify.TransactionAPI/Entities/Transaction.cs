using Microsoft.EntityFrameworkCore;
using Patrify.TransactionAPI.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;


namespace Patrify.TransactionAPI.Entities
{
    public class Transaction: BaseEntity
    {
        public int AccountId { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
