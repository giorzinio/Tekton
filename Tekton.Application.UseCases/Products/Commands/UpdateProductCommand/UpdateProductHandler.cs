using AutoMapper;
using MediatR;
using Tekton.Application.Interface.Persistence;
using Tekton.Domain.Entities;
using Tekton.Transversal.Common;

namespace Tekton.Application.UseCases.Products.Commands.UpdateProductCommand
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Response<bool>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();

            var product = _mapper.Map<Product>(request);
            await _unitOfWork.Product.UpdateAsync(product, cancellationToken);

            response.Data = await _unitOfWork.Save(cancellationToken) > 0;
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Actualización Exitosa!!!";
            }
            return response;
        }
    }
}
