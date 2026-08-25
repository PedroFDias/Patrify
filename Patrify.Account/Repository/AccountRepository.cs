using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Patrify.Account.Entities.Context;
using Patrify.Account.IRepository;

namespace Patrify.Account.Repository
{
    public class AccountRepository : Repository<Entities.Account, SQLServerContext>, IAccountRepository
    {
        public AccountRepository(SQLServerContext context) : base(context) { }

        public async Task UpdateAmount(Guid AccountID, decimal amount)
        {
            var accountDb = await FirstOrDefaultAsync(a => a.Id == AccountID);

            if (accountDb is null)
                throw new KeyNotFoundException($"Account {AccountID} not found.");

            accountDb.Balance = accountDb.Balance + amount;

            Update(accountDb);
        }
    }

    
}
