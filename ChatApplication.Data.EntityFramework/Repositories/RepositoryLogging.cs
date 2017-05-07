using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Logging;
using log4net;

namespace ChatApplication.Data.EntityFramework.Repositories
{
    public class RepositoryLogging<TEntity> : IRepositoryReader<TEntity>, IRepositoryWriter<TEntity> where TEntity : class
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IRepositoryReader<TEntity> _readerDelegate;
        private readonly IRepositoryWriter<TEntity> _writerDelegate;

        public RepositoryLogging(IRepositoryReader<TEntity> readerDelegate, IRepositoryWriter<TEntity> writeDelegate)
        {
            _readerDelegate = readerDelegate;
            _writerDelegate = writeDelegate;
        }
        public TEntity Get(long id)
        {
            _log.Repo($"Request to get entity of type {typeof(TEntity).Name} with id: {id}");
            return _readerDelegate.Get(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            _log.Repo($"Request to get all entities of type {typeof(TEntity).Name}");
            return _readerDelegate.GetAll();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            _log.Repo($"Request to get entity of type {typeof(TEntity).Name} by predicate: {predicate}");
            return _readerDelegate.Find(predicate);
        }

        public void Add(TEntity entity)
        {
            _log.Info($"Request to add entity of type {typeof(TEntity).Name}");
            _writerDelegate.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _log.Repo($"Request to add range of type {typeof(TEntity).Name}");
            _writerDelegate.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _log.Repo($"Request to remove entity of type {typeof(TEntity).Name}");
            _writerDelegate.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _log.Repo($"Request to remove entities of type {typeof(TEntity).Name}");
            _writerDelegate.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            _log.Repo($"Request to update entity of type {typeof(TEntity).Name}");
            _writerDelegate.Update(entity);
        }
    }
}
