using AutoMapper;
using MediatR;
using Tekton.Application.Interface.Persistence;
using Tekton.Domain.Entities;
using Tekton.Transversal.Common;

namespace Tekton.Application.UseCases.Products.Commands.CreateProductCommand
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Response<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            var product = _mapper.Map<Product>(request);
            await _unitOfWork.Product.InsertAsync(product, cancellationToken);

            response.Data = await _unitOfWork.Save(cancellationToken) > 0;
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro Exitoso!!!";
            }
            return response;
        }
    }
}
