using MediatR;
using Tekton.Transversal.Common;

namespace Tekton.Application.UseCases.Products.Commands.UpdateProductCommand
{
    public sealed record class UpdateProductCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusName { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
