namespace Patrify.TransactionAPI.Repository
{
    public class TransactionRepository : Repository<Transaction, SQLServerContext>, ITransactionRepository
    {
        public TransactionRepository(SQLServerContext context): base(context) { }
    }
}
