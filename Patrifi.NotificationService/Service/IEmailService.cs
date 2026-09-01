namespace Patrify.NotificationService.Service
{
    public interface IEmailService
    {
        Task SendAsync(
            string to,
            string subject,
            string html);
    }
}
