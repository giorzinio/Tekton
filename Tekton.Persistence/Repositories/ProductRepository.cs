using Microsoft.EntityFrameworkCore;
using Tekton.Domain.Entities;
using Tekton.Application.Interface.Persistence;
using Tekton.Persistence.Context;

namespace Tekton.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected readonly ApplicationDbContext _applicationDbContext;
        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.
                Set<Product>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public async Task<bool> InsertAsync(Product product, CancellationToken cancellationToken)
        {
            _applicationDbContext.Add(product);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Set<Product>().SingleOrDefaultAsync(x => x.Id.Equals(product.Id));
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            entity.Name = product.Name;
            entity.Description = product.Description;
            entity.Price = product.Price;
            entity.Stock = product.Stock;
            entity.StatusName = product.StatusName;

            _applicationDbContext.Update(entity);
            return await Task.FromResult(true);

        }
    }
}
