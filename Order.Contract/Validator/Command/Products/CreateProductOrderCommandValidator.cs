using FluentValidation;
using Order.Contract.Request.Command.Products;

namespace Order.Contract.Validator.Command.Products
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(r => r.Price).NotNull().NotEmpty();
            RuleFor(r => r.Quantity).NotNull().NotEmpty();
            RuleFor(r => r.Barcode).NotNull().NotEmpty().MaximumLength(50).WithErrorCode("Barcode Maximum Character is 50");
            RuleFor(r => r.Description).NotNull().NotEmpty().MaximumLength(100).WithErrorCode("Description Maximum Character is 100");
        }
    }
}
