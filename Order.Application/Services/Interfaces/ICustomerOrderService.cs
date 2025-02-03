using Order.Contract.Request.Command.CustomerOrders;
using Order.Contract.Request.Query.CustomerOrders;
using Order.Contract.Response.Command.CustomerOrders;
using Order.Contract.Response.Query.CustomerOrders;

namespace Order.ApplicationService.Services.Interfaces
{
    public interface ICustomerOrderService
    {
        Task<GetCustomerOrderByIdQueryResponse> GetCustomerOrderById(GetCustomerOrderByIdQueryRequest request, CancellationToken cancellationToken);
        Task<List<GetCustomerOrdersByCustomerIdQueryResponse>> GetCustomerOrdersByCustomerId(GetCustomerOrdersByCustomerIdQueryRequest request, CancellationToken cancellationToken);
        Task<CreateCustomerOrderCommandResponse> CreateCustomerOrder(CreateCustomerOrderCommandRequest request, CancellationToken cancellationToken);
        Task<UpdateCustomerOrderCommandResponse> UpdateCustomerOrder(UpdateCustomerOrderCommandRequest request, CancellationToken cancellationToken);
        Task<DeleteCustomerOrderCommandResponse> DeleteCustomerOrder(DeleteCustomerOrderCommandRequest request, CancellationToken cancellationToken);
    }
}
