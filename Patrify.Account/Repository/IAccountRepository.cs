using GenericRepository;
using Patrify.Account.Entities;
namespace Patrify.Account.IRepository
{
    public interface IAccountRepository : IRepository<Entities.Account> 
    {
        Task UpdateAmount(Guid AccountID, decimal amount);
    }
}
