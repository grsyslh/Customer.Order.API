using MediatR;
using Order.Contract.Response.Command.Products;

namespace Order.Contract.Request.Command.Products
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
