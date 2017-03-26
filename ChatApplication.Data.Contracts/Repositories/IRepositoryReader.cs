using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ChatApplication.Data.Contracts.Repositories
{
    public interface IRepositoryReader<TEntity> where TEntity : class
    {
        TEntity Get(long id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}
