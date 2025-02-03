using FluentValidation;
using Order.Contract.Request.Command.CustomerOrders;

namespace Order.Contract.Validator.Command.CustomerOrders
{
    public class DeleteCustomerOrderCommandValidator : AbstractValidator<DeleteCustomerOrderCommandRequest>
    {
        public DeleteCustomerOrderCommandValidator()
        {
        }
    }
}
