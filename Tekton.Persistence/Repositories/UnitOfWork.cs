using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Application.Interface.Persistence;
using Tekton.Persistence.Context;

namespace Tekton.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public IProductRepository Product { get; }


        public UnitOfWork(ApplicationDbContext applicationDbContext, IProductRepository product)
        {
            _applicationDbContext = applicationDbContext;
            this.Product = product;
        }
        public async Task<int> Save(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }

    }
}
