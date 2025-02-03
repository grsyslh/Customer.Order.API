using MediatR;
using Order.Contract.Response.Query.Products;

namespace Order.Contract.Request.Query.Products
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
