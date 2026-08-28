namespace Patrify.Account.Repository
{
    public class AccountRepository : Repository<UserAccount, SQLServerContext>, IAccountRepository
    {
        public AccountRepository(SQLServerContext context) : base(context) { }

        public async Task UpdateAmount(Guid AccountID, decimal amount)
        {
            var accountDb = await FirstOrDefaultAsync(a => a.Id == AccountID);

            if (accountDb is null)
                throw new KeyNotFoundException($"Account {AccountID} not found.");

            accountDb.Balance = accountDb.Balance + amount;
            accountDb.UpdateAt = DateTime.Now;

            Update(accountDb);
        }
    }

    
}
