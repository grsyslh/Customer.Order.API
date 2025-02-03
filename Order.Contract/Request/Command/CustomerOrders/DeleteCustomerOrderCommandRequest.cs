using MediatR;
using Order.Contract.Response.Command.CustomerOrders;

namespace Order.Contract.Request.Command.CustomerOrders
{
    public class DeleteCustomerOrderCommandRequest : IRequest<DeleteCustomerOrderCommandResponse>
    {
        public Guid CustomerOrderId { get; set; }
    }
}
