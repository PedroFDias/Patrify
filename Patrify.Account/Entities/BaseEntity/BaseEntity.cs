using System.ComponentModel.DataAnnotations;

namespace Patrify.Account.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}
