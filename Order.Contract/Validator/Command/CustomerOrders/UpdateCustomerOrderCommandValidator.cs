using FluentValidation;
using Order.Contract.Request.Command.CustomerOrders;
using Order.Contract.Validator.Command.Products;

namespace Order.Contract.Validator.Command.CustomerOrders
{
    public class UpdateCustomerOrderCommandValidator : AbstractValidator<UpdateCustomerOrderCommandRequest>
    {
        public UpdateCustomerOrderCommandValidator()
        {
            RuleFor(x => x.CustomerOrderId).NotNull().NotEmpty().WithMessage("Customer Order Id should be valid.");
            RuleFor(x => x.NewCustomerAddress).MaximumLength(200).When(x => !string.IsNullOrEmpty(x.NewCustomerAddress)).WithMessage("Customer Address max 200 character is allowed.");
            RuleForEach(x => x.Products).SetValidator(new ProductDtoValidator());
        }
    }
}
