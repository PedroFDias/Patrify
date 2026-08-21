using Microsoft.EntityFrameworkCore;
using Patrify.TransactionAPI.Entities;
using Patrify.TransactionAPI.Entities.Context;

namespace Patrify.TransactionAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        protected readonly SQLServerContext _context;
        protected readonly DbSet<Transaction> _dbSet;

        public TransactionRepository(SQLServerContext context)
        {
            _context = context;
            _dbSet = context.Set<Transaction>();
        }
        public async Task AddAsync(Transaction transaction)
        {
            await _dbSet.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
