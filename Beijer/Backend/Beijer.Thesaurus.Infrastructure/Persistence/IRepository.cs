using System;
using System.Linq;
using System.Linq.Expressions;

namespace Beijer.Thesaurus.Infrastructure.Persistence {

    public interface IRepository<TEntity> where TEntity : class {

        #region Properties

        TEntity this[long id] { get; set; }

        #endregion

        #region Methods

        IQueryable<TEntity> Query();

        TEntity Find(Expression<Func<TEntity, bool>> expression);

        TEntity Add(TEntity entity);

        void Delete(TEntity entity);

        #endregion

        #region Events

        #endregion

    }

}
