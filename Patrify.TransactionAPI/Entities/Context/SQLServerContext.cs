using Microsoft.EntityFrameworkCore;

namespace Patrify.TransactionAPI.Entities.Context
{
    public class SQLServerContext : DbContext
    {
        public SQLServerContext() { }
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
