using FluentValidation;
using Order.Contract.Request.Query.CustomerOrders;

namespace Order.Contract.Validator.Query.CustomerOrders
{
    public class GetCustomerOrdersByCustomerIdQueryValidator : AbstractValidator<GetCustomerOrdersByCustomerIdQueryRequest>
    {
        public GetCustomerOrdersByCustomerIdQueryValidator()
        {
        }
    }
}
