using FluentValidation;
using Tekon.Application.DTO;

namespace Tekton.Application.Validator
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator() 
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Stock).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
