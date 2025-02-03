using Order.ApplicationService.Services.Interfaces;
using Order.Queue.RabbitMQ.Producer;

namespace Order.ApplicationService.Services
{
    public class EmailService : ISenderService
    {
        private readonly RabbitMqService _rabbitMqService;
        public EmailService(RabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public async Task Send(string message, CancellationToken cancellationToken)
        {
            _rabbitMqService.SendMessage($"Email Service: {message}");
        }
    }
}
