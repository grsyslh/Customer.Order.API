using MediatR;
using Order.ApplicationService.Services.Interfaces;
using Order.Contract.Request.Query.CustomerOrders;
using Order.Contract.Response.Query.CustomerOrders;

namespace Order.ApplicationService.Handlers.QueryHandler.CustomerOrders
{
    public class GetCustomerOrdersByCustomerIdQueryHandler : IRequestHandler<GetCustomerOrdersByCustomerIdQueryRequest, List<GetCustomerOrdersByCustomerIdQueryResponse>>
    {
        private readonly ICustomerOrderService _customerOrderService;

        public GetCustomerOrdersByCustomerIdQueryHandler(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        public async Task<List<GetCustomerOrdersByCustomerIdQueryResponse>> Handle(GetCustomerOrdersByCustomerIdQueryRequest request, CancellationToken cancellationToken)
        {
            return await _customerOrderService.GetCustomerOrdersByCustomerId(request, cancellationToken);
        }
    }
}
