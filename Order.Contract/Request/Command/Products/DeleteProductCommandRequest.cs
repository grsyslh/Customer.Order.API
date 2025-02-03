using MediatR;
using Order.Contract.Response.Command.Products;

namespace Order.Contract.Request.Command.Products
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
    {
        public Guid ProductId { get; set; }
    }
}
