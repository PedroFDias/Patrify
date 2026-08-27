using Patrify.MessageBus.Contracts.Enums;
namespace Patrify.TransactionAPI.Entities
{
    public class Transaction: BaseEntity
    {
        public Guid AccountId { get; set; }
        public Guid? TargetAccountId { get; set; }

        [Precision(18, 2)]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
    }
}
