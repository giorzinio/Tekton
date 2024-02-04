using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekon.Application.DTO;
using Tekton.Transversal.Common;

namespace Tekton.Application.Interface.UseCases
{
    public interface IProductApplication
    {
        Task<Response<bool>> Create(ProductDto productDto, CancellationToken cancellationToken = default);
        Task<Response<bool>> Update(ProductDto productDto, CancellationToken cancellationToken = default);
        Task<Response<ProductDto>> GetById(int id, CancellationToken cancellationToken = default);

    }
}
