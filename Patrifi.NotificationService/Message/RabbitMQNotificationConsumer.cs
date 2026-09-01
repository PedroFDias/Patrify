using Patrify.MessageBus.Contracts.Events;
using Patrify.MessageBus.RabbitMQ.Consumer;
using Patrify.NotificationService.Service;

namespace Patrify.Account.Message
{
    public class RabbitMQNotificationConsumer : BackgroundService
    {
        private IRabbitMQConsumer _rabbitMQConsumer;
        private IServiceScopeFactory ServicesProvider;

        public RabbitMQNotificationConsumer(IRabbitMQConsumer rabbitMQConsumer, IServiceScopeFactory servicesProvider)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
            ServicesProvider = servicesProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbitMQConsumer.ConsumeAsync<TransferNotificationEvent>(
                "Patrify.exchange",
                "topic",
                "notification.transfer",
                "notification.transfer.*",
                async (message) =>
                {
                    try
                    {
                        Console.WriteLine("1 - Mensagem recebida");

                        using var scope = ServicesProvider.CreateScope();

                        var notificationService =
                            scope.ServiceProvider
                                .GetRequiredService<INotificationService>();

                        Console.WriteLine("2 - NotificationService encontrado");

                        var notification = new TransferNotificationEvent(
                            message.AccountId,
                            message.Name,
                            message.LastName,
                            message.Email,
                            $"{message.DestinationName}",
                            message.Amount,
                            NotificationType.TransferSent
                        );

                        await notificationService.CreateTransferNotificationAsync(message);

                        Console.WriteLine("3 - Notification criada");


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERRO:");
                        Console.WriteLine(ex);
                    }
                }
            );
        }
    }
}
