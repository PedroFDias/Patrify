using Patrify.MessageBus.Contracts.Events;
namespace Patrify.NotificationService.Service
{
    public interface INotificationService
    {
        Task CreateNotification<T>( T notification );

        Task CreateTransferNotificationAsync(TransferNotificationEvent message);

    }
}