using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekon.Application.DTO;
using Tekton.Application.Interface.Persistence;
using Tekton.Application.Interface.UseCases;
using Tekton.Transversal.Common;
using Tekton.Application.Validator;
using Tekton.Domain.Entities;
using AutoMapper;

namespace Tekton.Application.UseCases.Products
{
    public class ProductApplication : IProductApplication
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ProductDtoValidator _productValidator;

        public ProductApplication(IMapper mapper, IUnitOfWork unitOfWork, ProductDtoValidator discountDtoValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productValidator = discountDtoValidator;
        }

        public async Task<Response<bool>> Create(ProductDto productDto, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            var validation = await _productValidator.ValidateAsync(productDto, cancellationToken);
            if (!validation.IsValid)
            {
                response.Message = "Errores de Validación";
                response.Errors = validation.Errors;
                return response;
            }
            
            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.Product.InsertAsync(product, cancellationToken);

            response.Data = await _unitOfWork.Save(cancellationToken) > 0;
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Registro Exitoso!!!";
            }
            return response;
        }

        public async Task<Response<ProductDto>> GetById(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<ProductDto>();
            var product = await _unitOfWork.Product.GetByIdAsync(id, cancellationToken);
            if (product is null)
            {
                response.IsSuccess = true;
                response.Message = "Producto no existe...";
                return response;
            }

            response.Data = _mapper.Map<ProductDto>(product);
            response.IsSuccess = true;
            response.Message = "Consulta Exitosa!!!";
            return response;
        }

        public async Task<Response<bool>> Update(ProductDto productDto, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            var validation = await _productValidator.ValidateAsync(productDto, cancellationToken);
            if (!validation.IsValid)
            {
                response.Message = "Errores de Validación";
                response.Errors = validation.Errors;
                return response;
            }
            var product = _mapper.Map<Product>(productDto);
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
