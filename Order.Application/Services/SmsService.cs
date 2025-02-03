using Order.ApplicationService.Services.Interfaces;
using Order.Queue.RabbitMQ.Producer;

namespace Order.ApplicationService.Services
{
    public class SmsService : ISenderService
    {
        private readonly RabbitMqService _rabbitMqService;
        public SmsService(RabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public async Task Send(string message, CancellationToken cancellationToken)
        {
            _rabbitMqService.SendMessage($"Sms Service: {message}");
        }
    }
}
