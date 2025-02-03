namespace Order.ApplicationService.Services.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string notificationText, CancellationToken cancellationToken);
    }
}
