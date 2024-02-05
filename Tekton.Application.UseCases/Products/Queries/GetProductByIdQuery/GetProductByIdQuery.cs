using MediatR;
using Tekton.Domain.Dto;
using Tekton.Transversal.Common;

namespace Tekton.Application.UseCases.Products.Queries.GetProductByIdCommand
{
    public sealed record GetProductByIdQuery : IRequest<Response<ProductDto>>
    {
        public int Id { get; set; }
    }
}
