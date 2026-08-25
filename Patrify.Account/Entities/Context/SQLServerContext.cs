using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Patrify.Account.Entities.Context
{
    public class SQLServerContext: DbContext , IUnitOfWork
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
    }
}
