using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Beijer.Thesaurus.Infrastructure.Persistence {

    public class UnitOfWork : IUnitOfWork {

        #region Members

        private readonly DbContext context;
        private bool disposed = false;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public UnitOfWork(DbContext context) {
            this.context = context;
        }

        #endregion

        #region Methods

        public async Task CommitAsync() {
            await context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing) {

            if (!disposed) {

                if (disposing) {
                    context.Dispose();
                }
            }

            disposed = true;

        }

        public void Dispose() {

            Dispose(true);
            GC.SuppressFinalize(this);

        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class {
            return new Repository<TEntity>(context);
        }

        #endregion

        #region Events

        #endregion

    }

}