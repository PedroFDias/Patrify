namespace Patrify.Account.IRepository
{
    public interface IAccountRepository : IRepository<UserAccount> 
    {
        Task UpdateAmount(Guid AccountID, decimal amount);
    }
}
