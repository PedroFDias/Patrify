using GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace Patrify.NotificationService.Entities.Context
{
    public class SQLServerContext : DbContext, IUnitOfWork
    {
        public SQLServerContext(DbContextOptions<SQLServerContext> options) : base(options) { }
        public DbSet<Notification> Notifications { get; set; }
    }
}
