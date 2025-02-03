using MediatR;
using Order.ApplicationService.Services.Interfaces;
using Order.Contract.Request.Command.CustomerOrders;
using Order.Contract.Response.Command.CustomerOrders;

namespace Order.ApplicationService.Handlers.CommandHandler.CustomerOrders
{
    public class CreateCustomerOrderCommandHandler : IRequestHandler<CreateCustomerOrderCommandRequest, CreateCustomerOrderCommandResponse>
    {
        private readonly ICustomerOrderService _customerOrderService;

        public CreateCustomerOrderCommandHandler(ICustomerOrderService custumerOrderService)
        {
            _customerOrderService = custumerOrderService;
        }

        public async Task<CreateCustomerOrderCommandResponse> Handle(CreateCustomerOrderCommandRequest request, CancellationToken cancellationToken)
        {
            return await _customerOrderService.CreateCustomerOrder(request, cancellationToken);
        }
    }

}
