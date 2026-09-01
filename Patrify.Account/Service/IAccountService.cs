using Microsoft.AspNetCore.Mvc;

namespace Patrify.Account.Service
{
    public interface IAccountService
    {
        Task AddAsync(UserAccount account);
        Task<UserAccount?> GetAccountByCpf(string cpf);
    }
}
