using Order.ApplicationService.Services.Interfaces;

namespace Order.ApplicationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEnumerable<ISenderService> _senderServices;
        public NotificationService(IEnumerable<ISenderService> senderServices)
        {
            _senderServices = senderServices;
        }

        public async Task SendNotificationAsync(string notificationText, CancellationToken cancellationToken)
        {
            foreach (var service in _senderServices)
            {
                await service.Send(notificationText, cancellationToken);
            }
        }
    }
}
