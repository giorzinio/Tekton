using AutoMapper;
using MediatR;
using Tekton.Application.Interface.Persistence;
using Tekton.Domain.Dto;
using Tekton.Domain.Entities;
using Tekton.Transversal.Common;

namespace Tekton.Application.UseCases.Products.Queries.GetProductByIdCommand
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Response<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<ProductDto>();
            var states = await _unitOfWork.Product.GetStatusNameAsync();
            var product = await _unitOfWork.Product.GetByIdAsync(request.Id, cancellationToken);
            if (product is null)
            {
                response.IsSuccess = true;
                response.Message = "Producto no existe...";
                return response;
            }
            var discount = await _unitOfWork.Product.GetDiscountByProductIdAsync(product.Id);
            var productDto = _mapper.Map<ProductDto>(product);
            productDto.StatusName = states[product.StatusName.ToString()];
            productDto.Discount = discount.Discount;
            productDto.FinalPrice = productDto.Price * (100 - discount.Discount) / 100;
            response.Data = productDto;
            response.IsSuccess = true;
            response.Message = "Consulta Exitosa!!!";
            return response;
        }
    }
}
