using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Beijer.Thesaurus.Infrastructure.Persistence {

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {

        #region Members

        private readonly DbSet<TEntity> innerRepository;

        #endregion

        #region Properties

        public TEntity this[long id] {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region Constructors

        public Repository(DbContext context) {
            innerRepository = context.Set<TEntity>();
        }

        #endregion

        #region Methods

        public TEntity Add(TEntity entity) {

            var result = innerRepository.Add(entity);
            return result.Entity;

        }

        public TEntity Find(Expression<Func<TEntity, bool>> expression) {
            return innerRepository.FirstOrDefault(expression);
        }

        public IQueryable<TEntity> Query() {
            return innerRepository.AsQueryable();
        }

        public void Delete(TEntity entity) {
            innerRepository.Remove(entity);
        }

        #endregion

        #region Events

        #endregion

    }

}