using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Patrify.TransactionAPI.Entities;
using Patrify.TransactionAPI.Entities.Context;

namespace Patrify.TransactionAPI.Repositories
{
    public class TransactionRepository : Repository<Transaction, SQLServerContext>, ITransactionRepository
    {
        public TransactionRepository(SQLServerContext context): base(context) { }
    }
}
