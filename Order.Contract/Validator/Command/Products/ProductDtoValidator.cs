using FluentValidation;
using Order.Contract.Dtos;

namespace Order.Contract.Validator.Command.Products
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Product Quantity should be greater than 0.");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Product price cannot be lower or equal to 0.");
        }
    }
}
