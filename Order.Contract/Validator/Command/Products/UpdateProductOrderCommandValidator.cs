using FluentValidation;
using Order.Contract.Request.Command.Products;

namespace Order.Contract.Validator.Command.Products
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidator()
        {
        }
    }
}
