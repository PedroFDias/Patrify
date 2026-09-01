namespace Patrify.Account.IRepository
{
    public interface IAccountRepository : IRepository<UserAccount> 
    {
        Task UpdateAmount(Guid accountId, decimal amount);
    }
}
