using MediatR;
using Order.ApplicationService.Services.Interfaces;
using Order.Contract.Request.Query.Products;
using Order.Contract.Response.Query.Products;

namespace Order.ApplicationService.Handlers.QueryHandler.Products
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQueryRequest, List<GetProductsQueryResponse>>
    {
        private readonly IProductService _productService;

        public GetProductsQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<List<GetProductsQueryResponse>> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            return await _productService.GetProducts(cancellationToken);
        }
    }
}
