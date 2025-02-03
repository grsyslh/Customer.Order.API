using MediatR;
using Order.ApplicationService.Services.Interfaces;
using Order.Contract.Request.Query.Products;
using Order.Contract.Response.Query.Products;

namespace Order.ApplicationService.Handlers.QueryHandler.Products
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        private readonly IProductService _productService;

        public GetProductByIdQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            return await _productService.GetProductById(request, cancellationToken);
        }
    }
}
