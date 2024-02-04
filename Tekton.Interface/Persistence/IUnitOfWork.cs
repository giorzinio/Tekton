using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Application.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        Task<int> Save(CancellationToken cancellationToken);
    }
}
