using GenericRepository;
using Patrify.NotificationService.Entities;
using Patrify.NotificationService.Entities.Context;
using Patrify.NotificationService.Repository;

namespace Patrify.NotificationService.Repository
{
    public class NotificationRepository : Repository<Notification, SQLServerContext>, INotificationRepository
    {
        public NotificationRepository(SQLServerContext context): base(context) { }
    }
}
