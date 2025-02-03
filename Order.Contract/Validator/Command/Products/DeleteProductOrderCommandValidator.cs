using FluentValidation;
using Order.Contract.Request.Command.Products;

namespace Order.Contract.Validator.Command.Products
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
    {
        public DeleteProductCommandValidator()
        {
        }
    }
}
