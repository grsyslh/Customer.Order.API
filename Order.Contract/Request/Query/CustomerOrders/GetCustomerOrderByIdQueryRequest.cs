using MediatR;
using Order.Contract.Response.Query.CustomerOrders;

namespace Order.Contract.Request.Query.CustomerOrders
{
    public class GetCustomerOrderByIdQueryRequest : IRequest<GetCustomerOrderByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
