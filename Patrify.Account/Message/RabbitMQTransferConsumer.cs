namespace Patrify.Account.Message
{
    public class RabbitMQTransferConsumer : BackgroundService
    {
        private IRabbitMQConsumer _rabbitMQConsumer;
        private IServiceScopeFactory ServicesProvider;
        private IRabbitMQPublish _rabbitMQMessageSender;

        public RabbitMQTransferConsumer(IRabbitMQConsumer rabbitMQConsumer, IServiceScopeFactory servicesProvider, IRabbitMQPublish rabbitMQPublish)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
            ServicesProvider = servicesProvider;
            _rabbitMQMessageSender = rabbitMQPublish;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbitMQConsumer.ConsumeAsync<TransferCreatedEvent>(
                "Patrify.exchange",
                "topic",
                "account.transfer.created",
                "transfer.created",
                async (message) =>
                {
                    try
                    {
                        using var scope = ServicesProvider.CreateScope();
                        var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();
                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        var accountOrigem = accountRepository.GetByExpressionWithTracking(a => a.Id == message.AccountId);

                        if(accountOrigem?.Balance < message.Amount)
                        {
                            throw new InvalidOperationException("Saldo insuficiente");
                        }

                        await accountRepository.UpdateAmount(message.AccountId, -message.Amount);
                        await accountRepository.UpdateAmount(message.TargetAccountId, message.Amount);
                        await unitOfWork.SaveChangesAsync();

                        var messageSender = new TransactionStatusChangeEvent(message.TransactionId, TransactionStatus.Completed);

                        await _rabbitMQMessageSender.Publish(
                            messageSender,
                            "Patrify.exchange",
                            "topic",
                            "status.change"
                        );
                    }
                    catch
                    {
                        var messageSender = new TransactionStatusChangeEvent(message.TransactionId, TransactionStatus.Failed);

                        await _rabbitMQMessageSender.Publish(
                            messageSender,
                            "Patrify.exchange",
                            "topic",
                            "status.change"
                        );
                    }
                }
            );
        }
    }
}
