using Resend;

namespace Patrify.NotificationService.Service
{
    public class EmailService : IEmailService
    {
        private readonly IResend _resend;

        public EmailService(IResend resend)
        {
            _resend = resend;
        }

        public async Task SendAsync(
            string to,
            string subject,
            string html)
        {
            var message = new EmailMessage();

            message.From = "Patrify <onboarding@resend.dev>";
            message.To.Add(to);
            message.Subject = subject;
            message.HtmlBody = html;

            await _resend.EmailSendAsync(message);
        }
    }
}
