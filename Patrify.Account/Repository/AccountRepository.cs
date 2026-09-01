namespace Patrify.Account.Repository
{
    public class AccountRepository : Repository<UserAccount, SQLServerContext>, IAccountRepository
    {
        public AccountRepository(SQLServerContext context) : base(context) { }

        public async Task UpdateAmount(Guid accountId, decimal amount)
        {
            var accountDb = GetByExpressionWithTracking(a => a.Id == accountId);

            if (accountDb is null)
                throw new KeyNotFoundException($"Account {accountId} not found.");

            accountDb.Balance = accountDb.Balance + amount;
            accountDb.UpdateAt = DateTime.Now;

            Update(accountDb);
        }
    }

    
}
