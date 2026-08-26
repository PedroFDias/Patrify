namespace Patrify.Account.Services
{
    public interface IAccountService
    {
        Task AddAsync(UserAccount account);
    }
}
