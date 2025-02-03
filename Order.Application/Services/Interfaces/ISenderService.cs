namespace Order.ApplicationService.Services.Interfaces
{
    public interface ISenderService
    {
        Task Send(string message, CancellationToken cancellationToken);
    }
}
