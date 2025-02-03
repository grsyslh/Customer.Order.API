using AutoMapper;
using Order.ApplicationService.Services.Interfaces;
using Order.Caching.Redis.Services;
using Order.Contract.Request.Command.Products;
using Order.Contract.Request.Query.Products;
using Order.Contract.Response.Command.CustomerOrders;
using Order.Contract.Response.Command.Products;
using Order.Contract.Response.Query.Products;
using Order.DataAccess.Repository.Interfaces;
using Order.Domain.Entity;

namespace Order.ApplicationService.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IRepository<Product> _productRepository;
        private const string RedisCacheKey = "products";
        public ProductService(IMapper mapper, IRedisCacheService redisCacheService, IRepository<Product> productRepository)
        {
            _mapper = mapper;
            _redisCacheService = redisCacheService;
            _productRepository = productRepository;
        }

        public async Task<GetProductByIdQueryResponse> GetProductById(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _redisCacheService.GetAsync<List<Product>>(RedisCacheKey);

            if (products == null)
            {
                products = await _productRepository.GetAll(cancellationToken);
                await _redisCacheService.SetAsync(RedisCacheKey, products, TimeSpan.FromMinutes(10));
            }

            var product = products?.FirstOrDefault(x => x.Id == request.Id);

            if (product == null)
                throw new Exception("Product not found.");

            return _mapper.Map<GetProductByIdQueryResponse>(product);
        }

        public async Task<List<GetProductsQueryResponse>> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _redisCacheService.GetAsync<List<Product>>(RedisCacheKey);

            if (products == null)
            {
                products = await _productRepository.GetAll(cancellationToken);
                await _redisCacheService.SetAsync(RedisCacheKey, products, TimeSpan.FromMinutes(10));
            }

            return _mapper.Map<List<GetProductsQueryResponse>>(products);
        }

        public async Task<CreateProductCommandResponse> CreateProduct(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Create(_mapper.Map<Product>(request), cancellationToken);
            await _productRepository.SaveChangesAsync(cancellationToken);

            await _redisCacheService.RemoveAsync(RedisCacheKey);

            return _mapper.Map<CreateProductCommandResponse>(product);
        }

        public async Task<UpdateProductCommandResponse> UpdateProduct(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id, cancellationToken);

            if (product == null)
                throw new Exception("Product not found");

            product.Barcode = request.Barcode;
            product.Description = request.Description;
            product.Quantity = request.Quantity;
            product.Price = request.Price;

            _productRepository.Update(product);

            await _productRepository.SaveChangesAsync(cancellationToken);

            await _redisCacheService.RemoveAsync(RedisCacheKey);

            return _mapper.Map<UpdateProductCommandResponse>(product);
        }

        public async Task<DeleteProductCommandResponse> DeleteProduct(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.ProductId, cancellationToken);

            if (product == null)
                throw new Exception("Product not found");

            _productRepository.Remove(product);
            await _productRepository.SaveChangesAsync(cancellationToken);

            await _redisCacheService.RemoveAsync(RedisCacheKey);

            return new DeleteProductCommandResponse { IsSuccess = true };
        }

    }
}
