using MediatR;
using Order.Contract.Dtos;
using Order.Contract.Response.Command.CustomerOrders;

namespace Order.Contract.Request.Command.CustomerOrders
{
    public class CreateCustomerOrderCommandRequest : IRequest<CreateCustomerOrderCommandResponse>
    {
        public Guid CustomerId { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
