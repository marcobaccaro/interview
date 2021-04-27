using System;
using System.Threading.Tasks;

namespace Beijer.Thesaurus.Infrastructure.Persistence {

    public interface IUnitOfWork : IDisposable {

        Task CommitAsync();

        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

    }
}