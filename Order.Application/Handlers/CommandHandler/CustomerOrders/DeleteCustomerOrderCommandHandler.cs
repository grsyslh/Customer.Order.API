using MediatR;
using Order.ApplicationService.Services.Interfaces;
using Order.Contract.Request.Command.CustomerOrders;
using Order.Contract.Response.Command.CustomerOrders;

namespace Order.ApplicationService.Handlers.CommandHandler.CustomerOrders
{
    public class DeleteCustomerOrderCommandHandler : IRequestHandler<DeleteCustomerOrderCommandRequest, DeleteCustomerOrderCommandResponse>
    {
        private readonly ICustomerOrderService _customerOrderService;

        public DeleteCustomerOrderCommandHandler(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        public async Task<DeleteCustomerOrderCommandResponse> Handle(DeleteCustomerOrderCommandRequest request, CancellationToken cancellationToken)
        {
            return await _customerOrderService.DeleteCustomerOrder(request, cancellationToken);
        }
    }

}
