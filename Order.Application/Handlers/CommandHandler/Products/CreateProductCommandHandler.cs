using MediatR;
using Order.ApplicationService.Services.Interfaces;
using Order.Contract.Request.Command.Products;
using Order.Contract.Response.Command.Products;

namespace Order.ApplicationService.Handlers.CommandHandler.Products
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductService _productService;

        public CreateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            return await _productService.CreateProduct(request, cancellationToken);
        }
    }

}
