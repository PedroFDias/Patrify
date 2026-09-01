namespace Patrify.MessageBus.Contracts.Events
{
    public record TransferNotificationEvent
    (
        Guid AccountId,
        string Name,
        string LastName,
        string Email,
        string DestinationName,
        decimal Amount,
        NotificationType Type
    );

    public enum NotificationType
    {
       TransferSent,
       TransferReceived,
    }
}
