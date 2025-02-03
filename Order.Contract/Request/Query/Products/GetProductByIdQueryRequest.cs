using MediatR;
using Order.Contract.Response.Query.Products;

namespace Order.Contract.Request.Query.Products
{
    public class GetProductsQueryRequest : IRequest<List<GetProductsQueryResponse>>
    {
    }
}
