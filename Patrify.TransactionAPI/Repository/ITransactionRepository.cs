using Patrify.TransactionAPI.Entities;

namespace Patrify.TransactionAPI.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction transaction);
    }
}
