using Patrify.MessageBus.Contracts.Enums;
using Patrify.MessageBus.RabbitMQ.Consumer;

namespace Patrify.TransactionAPI.Message
{
    public class TransactionChangeStatusConsumer : BackgroundService
    {
        private IRabbitMQConsumer _rabbitMQConsumer;
        private IServiceScopeFactory _serviceProvider;

        public TransactionChangeStatusConsumer(IRabbitMQConsumer rabbitMQConsumer, IServiceScopeFactory serviceScopeFactory)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
            _serviceProvider = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbitMQConsumer.ConsumeAsync<TransactionStatusChangeEvent>(
                "Patrify.exchange",
                "topic",
                "transaction.status.change",
                "status.change",
                async (message) =>{
                    try
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var transactionRepository = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        var transaction = transactionRepository.GetByExpressionWithTracking(t => t.Id == message.TransactionId);
                        transaction.Status = message.Status;
                        transaction.UpdatedDate = DateTime.Now;

                        await unitOfWork.SaveChangesAsync();
                    }
                    catch(Exception ex)
                    {
                        // Log the exception
                        throw new Exception($"Error processing message: {ex.Message}", ex);
                    }
                }
            );
        }
    }
}
