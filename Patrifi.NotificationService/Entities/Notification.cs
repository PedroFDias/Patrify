using Patrify.NotificationService.Entities.Base;
using Patrify.MessageBus.Contracts.Enums;
namespace Patrify.NotificationService.Entities
{
    public class Notification: BaseEntity
    {
        public Guid AccountId { get; set; }
        public Guid? TargetAccountId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
