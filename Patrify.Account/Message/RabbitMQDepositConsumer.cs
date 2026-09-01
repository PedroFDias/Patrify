namespace Patrify.Account.Message
{
    public class RabbitMQDepositConsumer : BackgroundService
    {
        private IRabbitMQConsumer _rabbitMQConsumer;
        private IServiceScopeFactory _servicesProvider;
        private IRabbitMQPublish _rabbitMQMessageSender;
        public RabbitMQDepositConsumer(IRabbitMQConsumer rabbitMQConsumer, IServiceScopeFactory servicesProvider, IRabbitMQPublish rabbitMQPublish)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
            _servicesProvider = servicesProvider;
            _rabbitMQMessageSender = rabbitMQPublish;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           await _rabbitMQConsumer.ConsumeAsync<DepositCreatedEvent>(
               "Patrify.exchange", 
               "topic",
               "account.deposit.created", 
               "deposit.created", 
               async (message) =>
                {
                    try
                    {
                        using var scope = _servicesProvider.CreateScope();

                        var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();

                        var account = accountRepository.GetByExpressionWithTracking(a => a.Id == message.AccountId);

                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        await accountRepository.UpdateAmount(account.Id, message.Amount);

                        await unitOfWork.SaveChangesAsync();

                        var messageSender = new TransactionStatusChangeEvent(message.TransactionId, TransactionStatus.Completed);

                        await _rabbitMQMessageSender.Publish(
                            messageSender,
                            "Patrify.exchange",
                            "topic",
                            "status.change"
                        );
                        
                    }
                    catch (Exception ex)
                    {
                        var messageSender = new TransactionStatusChangeEvent(message.TransactionId, TransactionStatus.Failed);

                        await _rabbitMQMessageSender.Publish(
                            messageSender,
                            "Patrify.exchange",
                            "topic",
                            "status.change"
                        );

                        Console.WriteLine($"Erro: {ex}");
                        throw;
                    }
                });
        }
    }
}
