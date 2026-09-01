using Microsoft.AspNetCore.Mvc;
using Patrify.NotificationService.Service;

namespace Patrify.NotificationService.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private INotificationService _serviceNotfication;
        public NotificationController(INotificationService serviceNotfication)
        {
            _serviceNotfication = serviceNotfication;
        }

        [HttpPost]
        public async Task SendNotification([FromBody] object notification)
        {
            await _serviceNotfication.CreateNotification(notification);
        }
    }
}
