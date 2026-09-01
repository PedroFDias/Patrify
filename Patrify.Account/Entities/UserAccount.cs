namespace Patrify.Account.Entities
{
    public class UserAccount : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        [Precision(18, 2)]
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }

        public UserAccount(string name, string lastName, string email, string cpf)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Cpf = cpf;
            Balance = 0;
            IsActive = true;
        }
    }
}
