namespace Patrify.Account.Entities
{
    public class UserAccount : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        [Precision(18, 2)]
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public UserAccount(string name)
        {
            Name = name;
            Balance = 0;
            IsActive = true;
        }
    }
}
