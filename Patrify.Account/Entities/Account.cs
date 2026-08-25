using Microsoft.EntityFrameworkCore;
using Patrify.Account.Entities.Base;

namespace Patrify.Account.Entities
{
    public class Account : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        [Precision(18, 2)]
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public Account(string name)
        {
            Name = name;
            Balance = 0;
            IsActive = true;
        }
    }
}
