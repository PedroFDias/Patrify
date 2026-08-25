using Patrify.TransactionAPI.Entities;

namespace Patrify.TransactionAPI.Service
{
    public interface ITransactionService
    {
        Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
    }
}