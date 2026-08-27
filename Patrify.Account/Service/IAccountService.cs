namespace Patrify.Account.Service
{
    public interface IAccountService
    {
        Task AddAsync(UserAccount account);
    }
}
