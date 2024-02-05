using Microsoft.EntityFrameworkCore;
using Tekton.Domain.Entities;
using Tekton.Application.Interface.Persistence;
using Tekton.Persistence.Context;
using LazyCache;
using System.Security.Policy;
using Tekton.Domain.Dto;
using Newtonsoft.Json;

namespace Tekton.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IAppCache _cache;
        protected readonly ApplicationDbContext _applicationDbContext;
        public ProductRepository(ApplicationDbContext applicationDbContext, IAppCache cache)
        {
            _applicationDbContext = applicationDbContext;
            _cache = cache;
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.
                Set<Product>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
        }

        public async Task<Dictionary<string, string>> GetStatusNameAsync()
        {
            string cacheKey = "ProductStatesName";
            return await _cache.GetOrAddAsync(cacheKey,() => Task.FromResult(NamesStatus()), DateTimeOffset.Now.AddMinutes(30));
        }

        public async Task<bool> InsertAsync(Product product, CancellationToken cancellationToken)
        {
            _applicationDbContext.Add(product);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Set<Product>().AsNoTracking().SingleOrDefaultAsync(x => x.Id.Equals(product.Id), cancellationToken);
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

        public async Task<DiscountDto> GetDiscountByProductIdAsync(int id)
        {
            using var httpClient = new HttpClient();
            var resp = await httpClient.GetStringAsync($"https://65bc243e52189914b5bda289.mockapi.io/api/descuento/discounts/{id}");
            return JsonConvert.DeserializeObject<DiscountDto>(resp);
        }         

        private Dictionary<string, string> NamesStatus()
        {
            var states = new Dictionary<string, string>(){
                {"1", "Active"},
                {"0", "Inactive"}
            };
            return states;
        }
    }
}
