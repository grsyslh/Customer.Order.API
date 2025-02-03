using MediatR;
using Order.Contract.Dtos;
using Order.Contract.Response.Command.CustomerOrders;

namespace Order.Contract.Request.Command.CustomerOrders
{
    public class UpdateCustomerOrderCommandRequest : IRequest<UpdateCustomerOrderCommandResponse>
    {
        public Guid CustomerOrderId { get; set; }
        public List<ProductDto> Products { get; set; }
        public List<Guid> RemovedProductIds { get; set; }
        public string NewCustomerAddress { get; set; }



    }
}


