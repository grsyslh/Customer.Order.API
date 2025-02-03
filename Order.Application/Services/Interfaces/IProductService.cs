using Order.Contract.Request.Command.Products;
using Order.Contract.Request.Query.Products;
using Order.Contract.Response.Command.Products;
using Order.Contract.Response.Query.Products;

namespace Order.ApplicationService.Services.Interfaces
{
    public interface IProductService
    {
        Task<GetProductByIdQueryResponse> GetProductById(GetProductByIdQueryRequest request, CancellationToken cancellationToken);
        Task<List<GetProductsQueryResponse>> GetProducts(CancellationToken cancellationToken);
        Task<CreateProductCommandResponse> CreateProduct(CreateProductCommandRequest request, CancellationToken cancellationToken);
        Task<UpdateProductCommandResponse> UpdateProduct(UpdateProductCommandRequest request, CancellationToken cancellationToken);
        Task<DeleteProductCommandResponse> DeleteProduct(DeleteProductCommandRequest request, CancellationToken cancellationToken);
    }
}
