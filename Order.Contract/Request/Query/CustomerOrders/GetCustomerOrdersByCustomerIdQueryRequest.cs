using MediatR;
using Order.Contract.Response.Query.CustomerOrders;

namespace Order.Contract.Request.Query.CustomerOrders
{
    public class GetCustomerOrdersByCustomerIdQueryRequest : IRequest<List<GetCustomerOrdersByCustomerIdQueryResponse>>
    {
        public Guid CustomerId { get; set; }
    }
}
