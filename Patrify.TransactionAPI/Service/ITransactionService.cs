
namespace Patrify.TransactionAPI.Service
{
    public interface ITransactionService
    {
        Task<bool> AddDepositAsync(DepositRequest transaction, CancellationToken cancellationToken = default);
        Task<bool> AddTransferAsync(TransferRequest transaction, CancellationToken cancellationToken = default);
    }
}