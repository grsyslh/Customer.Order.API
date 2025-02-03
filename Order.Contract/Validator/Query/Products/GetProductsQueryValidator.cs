using FluentValidation;
using Order.Contract.Request.Query.Products;

namespace Order.Contract.Validator.Query.Products
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQueryRequest>
    {
        public GetProductByIdQueryValidator()
        {
        }
    }
}
