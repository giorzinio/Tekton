using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Application.UseCases.Products.Commands.CreateProductCommand
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            var conditionsStatus = new List<int>() { 0, 1 };

            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotNull().NotEmpty();
            RuleFor(x => x.Stock).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Price).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.StatusName).NotNull()
                .Must(x => conditionsStatus.Contains(x))
                .WithMessage("StatusName solo permite valores : " + String.Join(", ", conditionsStatus));
        }
    }
}
