using FluentValidation;
using Order.Contract.Request.Command.CustomerOrders;

namespace Order.Contract.Validator.Command.CustomerOrders
{
    public class CreateCustomerOrderCommandValidator : AbstractValidator<CreateCustomerOrderCommandRequest>
    {
        public CreateCustomerOrderCommandValidator()
        {
            RuleFor(r => r.CustomerId).NotNull().NotEmpty().WithErrorCode("Customer Id Cannot be null");
        }
    }
}
