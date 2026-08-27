
namespace Patrify.TransactionAPI.Service
{
    public interface ITransactionService
    {
        Task AddDepositAsync(DepositRequest transaction, CancellationToken cancellationToken = default);
        Task AddTransferAsync(TransferRequest transaction, CancellationToken cancellationToken = default);
    }
}