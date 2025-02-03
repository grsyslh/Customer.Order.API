using MediatR;
using Order.ApplicationService.Services.Interfaces;
using Order.Contract.Request.Command.CustomerOrders;
using Order.Contract.Response.Command.CustomerOrders;

namespace Order.ApplicationService.Handlers.CommandHandler.CustomerOrders
{
    public class UpdateCustomerOrderCommandHandler : IRequestHandler<UpdateCustomerOrderCommandRequest, UpdateCustomerOrderCommandResponse>
    {
        private readonly ICustomerOrderService _customerOrderService;

        public UpdateCustomerOrderCommandHandler(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        public async Task<UpdateCustomerOrderCommandResponse> Handle(UpdateCustomerOrderCommandRequest request, CancellationToken cancellationToken)
        {
            return await _customerOrderService.UpdateCustomerOrder(request, cancellationToken);
        }
    }

}
