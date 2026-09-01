using Patrify.MessageBus.Contracts.Enums;
using Patrify.MessageBus.Contracts.Events;
using Patrify.NotificationService.Entities;
using Patrify.NotificationService.Repository;
using Resend;

namespace Patrify.NotificationService.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IEmailService _emailService;

        public NotificationService(
            INotificationRepository notificationRepository,
            IEmailService emailService)
        {
            _notificationRepository = notificationRepository;
            _emailService = emailService;
        }

        public async Task CreateTransferNotificationAsync(TransferNotificationEvent message)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                AccountId = message.AccountId,
                Title = "Sua transferência foi realizada",
                Message = $"Sua transferência de R$ {message.Amount:N2} foi realizada com sucesso.",
                Type = TransactionType.Transfer,
                CreatedAt = DateTime.UtcNow
            };

            await _notificationRepository.AddAsync(notification);

            var html = await GetTemplateNotification("TransferTemplate.html");

            html = html
                .Replace("{{NAME}}", message.Name + " " + message.LastName)
                .Replace("{{DESTINATION}}", message.DestinationName)
                .Replace("{{AMOUNT}}", message.Amount.ToString("N2"))
                .Replace(
                    "{{DATE}}",
                    DateTime.Now.ToString("HH:mm, dd/MM/yyyy"));

            await _emailService.SendAsync(
                message.Email,
                "Sua transferência foi realizada",
                html);
        }

        private static async Task<string> GetTemplateNotification(string templateName)
        {
            var path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Templates",
                templateName);

            return await File.ReadAllTextAsync(path);
        }

        public Task CreateNotification<T>(T notification)
        {
            throw new NotImplementedException();
        }
    }
}
