using GenericRepository;
using Patrify.TransactionAPI.Entities;
using Patrify.TransactionAPI.Repositories;

namespace Patrify.TransactionAPI.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            await _transactionRepository.AddAsync(transaction, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);                                                                                                                                                
        }
    }
}
