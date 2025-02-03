using MediatR;
using Order.ApplicationService.Services.Interfaces;
using Order.Contract.Request.Query.CustomerOrders;
using Order.Contract.Response.Query.CustomerOrders;

namespace Order.ApplicationService.Handlers.QueryHandler.CustomerOrders
{
    public class GetCustomerOrderByIdQueryHandler : IRequestHandler<GetCustomerOrderByIdQueryRequest, GetCustomerOrderByIdQueryResponse>
    {
        private readonly ICustomerOrderService _customerOrderService;

        public GetCustomerOrderByIdQueryHandler(ICustomerOrderService customerOrderService)
        {
            _customerOrderService = customerOrderService;
        }

        public async Task<GetCustomerOrderByIdQueryResponse> Handle(GetCustomerOrderByIdQueryRequest request, CancellationToken cancellationToken)
        {
            return await _customerOrderService.GetCustomerOrderById(request, cancellationToken);
        }
    }
}
