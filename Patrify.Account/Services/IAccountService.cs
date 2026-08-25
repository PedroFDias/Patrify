namespace Patrify.Account.Services
{
    public interface IAccountService
    {
        Task AddAsync(Entities.Account account);
    }
}
