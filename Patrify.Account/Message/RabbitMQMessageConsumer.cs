using GenericRepository;
using Patrify.Account.DTO;
using Patrify.Account.IRepository;
using Patrify.MessageBus.RabbitMQ.Consumer;
using RabbitMQ.Client;

namespace Patrify.Account.Message
{
    public class RabbitMQMessageConsumer : BackgroundService
    {
        private IRabbitMQConsumer _rabbitMQConsumer;
        private IServiceScopeFactory ServicesProvider;
        public RabbitMQMessageConsumer(IRabbitMQConsumer rabbitMQConsumer, IServiceScopeFactory servicesProvider)
        {
            _rabbitMQConsumer = rabbitMQConsumer;
            ServicesProvider = servicesProvider;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           await _rabbitMQConsumer.ConsumeAsync<TransactionCreatedEvent>(
               "Patrify.exchange", 
               "topic",
               "account.transaction.created", 
               "transaction.created", 
               async (message) =>
                {
                    try
                    {
                        Console.WriteLine("Mensagem recebida");

                        using var scope = ServicesProvider.CreateScope();

                        var accountRepository = scope.ServiceProvider.GetRequiredService<IAccountRepository>();

                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                        await accountRepository.UpdateAmount(message.AccountId, message.Amount);
                        Console.WriteLine("Account atualizada");

                        await unitOfWork.SaveChangesAsync();
                        Console.WriteLine("UnitOfWork salva");

                        Console.WriteLine("Handler terminou");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro: {ex}");
                        throw;
                    }
                });
        }
    }
}
