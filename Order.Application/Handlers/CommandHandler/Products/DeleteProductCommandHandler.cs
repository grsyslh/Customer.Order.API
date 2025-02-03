using MediatR;
using Order.ApplicationService.Services.Interfaces;
using Order.Contract.Request.Command.Products;
using Order.Contract.Response.Command.Products;

namespace Order.ApplicationService.Handlers.CommandHandler.Products
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProduct(request, cancellationToken);
        }
    }

}
