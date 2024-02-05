using MediatR;
using Tekton.Application.UseCases.Enums;
using Tekton.Transversal.Common;

namespace Tekton.Application.UseCases.Products.Commands.CreateProductCommand
{
    public sealed record class CreateProductCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; }
        public ProductStatusEnum StatusName { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
