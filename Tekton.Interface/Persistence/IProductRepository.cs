using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Domain.Dto;
using Tekton.Domain.Entities;

namespace Tekton.Application.Interface.Persistence
{
    public interface IProductRepository
    {
        Task<bool> InsertAsync(Product product, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Dictionary<string, string>> GetStatusNameAsync();
        Task<DiscountDto> GetDiscountByProductIdAsync(int id);
    }
}
