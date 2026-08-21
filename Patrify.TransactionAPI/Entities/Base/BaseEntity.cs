using System.ComponentModel.DataAnnotations;

namespace Patrify.TransactionAPI.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

    }
}
