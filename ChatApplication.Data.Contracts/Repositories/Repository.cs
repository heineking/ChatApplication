using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Contracts.Repositories
{
    public class Repository<TEntity> : IRepositoryReader<TEntity>, IRepositoryWriter<TEntity> where TEntity : class
    {
        protected readonly IRepositoryReader<TEntity> ReaderDelegate;
        protected readonly IRepositoryWriter<TEntity> WriterDelegate;

        protected Repository(IRepositoryReader<TEntity> reader, IRepositoryWriter<TEntity> writer)
        {
            ReaderDelegate = reader;
            WriterDelegate = writer;
        }
        public TEntity Get(long id)
        {
            return ReaderDelegate.Get(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return ReaderDelegate.GetAll();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return ReaderDelegate.Find(predicate);
        }

        public void Add(TEntity entity)
        {
            WriterDelegate.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            WriterDelegate.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            WriterDelegate.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            WriterDelegate.RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            WriterDelegate.Update(entity);
        }
    }
}
