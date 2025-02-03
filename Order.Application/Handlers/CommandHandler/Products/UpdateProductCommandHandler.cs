using MediatR;
using Order.ApplicationService.Services.Interfaces;
using Order.Contract.Request.Command.Products;
using Order.Contract.Response.Command.Products;

namespace Order.ApplicationService.Handlers.CommandHandler
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductService _productService;

        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProduct(request, cancellationToken);
        }
    }

}
