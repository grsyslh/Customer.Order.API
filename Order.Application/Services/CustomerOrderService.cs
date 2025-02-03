using AutoMapper;
using Order.ApplicationService.Services.Interfaces;
using Order.Caching.Redis.Services;
using Order.Contract.Request.Command.CustomerOrders;
using Order.Contract.Request.Query.CustomerOrders;
using Order.Contract.Response.Command.CustomerOrders;
using Order.Contract.Response.Command.Products;
using Order.Contract.Response.Query.CustomerOrders;
using Order.DataAccess.Repository.Interfaces;
using Order.Domain.Entity;

namespace Order.ApplicationService.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<CustomerOrder> _customerOrderRepository;


        public CustomerOrderService(IMapper mapper,
            INotificationService notificationService,
            IRepository<Product> productRepository,
            IRepository<Customer> customerRepository,
            IRepository<CustomerOrder> customerOrderRepository)
        {
            _mapper = mapper;
            _notificationService = notificationService;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _customerOrderRepository = customerOrderRepository;
        }

        public async Task<GetCustomerOrderByIdQueryResponse> GetCustomerOrderById(
            GetCustomerOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var customerOrder = await _customerOrderRepository.GetById(request.Id, cancellationToken, x => x.Products);

            if (customerOrder == null)
                throw new Exception("Customer order not found.");

            return _mapper.Map<GetCustomerOrderByIdQueryResponse>(customerOrder);
        }

        public async Task<List<GetCustomerOrdersByCustomerIdQueryResponse>> GetCustomerOrdersByCustomerId(
            GetCustomerOrdersByCustomerIdQueryRequest request, CancellationToken cancellationToken)
        {
            var customerOrderList =
                await _customerOrderRepository.GetByFilterList(x => x.CustomerId == request.CustomerId,
                    cancellationToken, true, x => x.Products);

            if (customerOrderList == null)
                throw new Exception("Customer order not found.");

            return _mapper.Map<List<GetCustomerOrdersByCustomerIdQueryResponse>>(customerOrderList);
        }

        public async Task<CreateCustomerOrderCommandResponse> CreateCustomerOrder(
            CreateCustomerOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetById(request.CustomerId, cancellationToken);

            if (customer == null)
                throw new Exception("Customer not found.");

            if (request.Products == null || !request.Products.Any())
                throw new Exception("Please specify products.");

            var products = new List<Product>();
            products.AddRange(_mapper.Map<List<Product>>(request.Products));

            var customerOrder = new CustomerOrder();
            customerOrder.CustomerId = request.CustomerId;
            customerOrder.Products = products;
            customerOrder.OrderDate = DateTime.Now.ToUniversalTime();

            var customerOrderEntity = await _customerOrderRepository.Create(customerOrder, cancellationToken);
            await _customerOrderRepository.SaveChangesAsync(cancellationToken);

            await _notificationService.SendNotificationAsync($"Dear {customer.Name}, your order has been received.",
                cancellationToken);

            return _mapper.Map<CreateCustomerOrderCommandResponse>(customerOrderEntity);
        }

        public async Task<UpdateCustomerOrderCommandResponse> UpdateCustomerOrder(
            UpdateCustomerOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var customerOrder = await _customerOrderRepository.GetById(request.CustomerOrderId, cancellationToken,
                x => x.Products, x => x.Customer);

            if (customerOrder == null)
            {
                throw new Exception("Customer Order Not Found.");
            }

            if (!string.IsNullOrEmpty(request.NewCustomerAddress))
            {
                var customer = await _customerRepository.GetById(customerOrder.CustomerId, cancellationToken);
                customer.Address = request.NewCustomerAddress;

                _customerRepository.Update(customer);

                await _notificationService.SendNotificationAsync(
                    $"Dear {customerOrder.Customer.Name}, your address has been updated.", cancellationToken);
            }

            if (request.RemovedProductIds != null && request.RemovedProductIds.Any())
            {
                var productsToRemove = customerOrder.Products.Where(p => request.RemovedProductIds.Contains(p.Id))
                    .ToList();
                foreach (var product in productsToRemove)
                {
                    product.Orders = null;
                    _productRepository.Remove(product);

                    await _notificationService.SendNotificationAsync(
                        $"Dear {customerOrder.Customer.Name}, that products you cancelled have been removed.",
                        cancellationToken);
                }
            }

            if (request.Products != null && request.Products.Any())
            {
                foreach (var addOrUpdateProduct in request.Products)
                {
                    var existingProduct = customerOrder.Products.FirstOrDefault(p => p.Id == addOrUpdateProduct.Id);
                    if (existingProduct != null)
                    {
                        existingProduct.Barcode = addOrUpdateProduct.Barcode;
                        existingProduct.Description = addOrUpdateProduct.Description;
                        existingProduct.Quantity = addOrUpdateProduct.Quantity;
                        existingProduct.Price = addOrUpdateProduct.Price;
                        existingProduct.Orders = null;

                        _productRepository.Update(existingProduct);

                        await _notificationService.SendNotificationAsync(
                            $"Dear {customerOrder.Customer.Name}, your basket has been updated.", cancellationToken);
                    }
                    else
                    {
                        var newproductDto = await _productRepository.Create(_mapper.Map<Product>(addOrUpdateProduct),
                            cancellationToken);
                        customerOrder.Products.Add(newproductDto);
                        _customerOrderRepository.Update(customerOrder);
                        await _notificationService.SendNotificationAsync(
                            $"Dear {customerOrder.Customer.Name}, new products you purchased have been added.",
                            cancellationToken);
                    }
                }
            }

            await _customerRepository.SaveChangesAsync(cancellationToken);
            await _productRepository.SaveChangesAsync(cancellationToken);
            await _customerOrderRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UpdateCustomerOrderCommandResponse>(customerOrder);
        }

        public async Task<DeleteCustomerOrderCommandResponse> DeleteCustomerOrder(
            DeleteCustomerOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var customerOrder =
                await _customerOrderRepository.GetById(request.CustomerOrderId, cancellationToken, x => x.Customer);
            if (customerOrder == null)
            {
                throw new Exception("Customer Order Not Found.");
            }

            _customerOrderRepository.Remove(customerOrder);
            await _customerOrderRepository.SaveChangesAsync(cancellationToken);

            await _notificationService.SendNotificationAsync(
                $"Dear {customerOrder.Customer.Name}, new products you purchased have been added.", cancellationToken);

            return new DeleteCustomerOrderCommandResponse { IsSuccess = true };
        }
    }
}