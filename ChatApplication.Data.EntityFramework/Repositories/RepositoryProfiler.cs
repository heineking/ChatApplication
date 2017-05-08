using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Logging;
using log4net;
using Newtonsoft.Json;

namespace ChatApplication.Data.EntityFramework.Repositories
{
    public class RepositoryProfiler<TEntity> : IRepositoryReader<TEntity>, IRepositoryWriter<TEntity> where TEntity : class
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IProfiler _profiler;
        private readonly IRepositoryReader<TEntity> _readerDelegate;
        private readonly IRepositoryWriter<TEntity> _writerDelegate;
        private string Type => typeof(TEntity).Name;

        public RepositoryProfiler(IRepositoryReader<TEntity> readerDelegate, IRepositoryWriter<TEntity> writeDelegate, IProfiler profiler)
        {
            _readerDelegate = readerDelegate;
            _writerDelegate = writeDelegate;

            // set up profiler
            _profiler = profiler;
            _profiler.Log = _log;
            _profiler.Type = typeof(TEntity).Name;
        }
        public TEntity Get(long id)
        {
            return _profiler.Profile(
                () => _readerDelegate.Get(id),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; id=[{id}]; "
            );
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _profiler.Profile(
                () => _readerDelegate.GetAll(),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; "    
            );
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _profiler.Profile(
                () => _readerDelegate.Find(predicate),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; predicate=[{predicate}]; "    
            );
        }

        public void Add(TEntity entity)
        {
            _profiler.Profile(
                () => _writerDelegate.Add(entity),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; entity=[{_profiler.Serialize(entity)}]; "
            );
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _profiler.Profile(
                () => _writerDelegate.AddRange(entities),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; entities=[{_profiler.Serialize(entities)}]; "
            );
        }

        public void Remove(TEntity entity)
        {
            _profiler.Profile(
                () => _writerDelegate.Remove(entity),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; entity=[{_profiler.Serialize(entity)}]; "
            );
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _profiler.Profile(
                () => _writerDelegate.RemoveRange(entities),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; entities=[{_profiler.Serialize(entities)}]; "
            );
        }

        public void Update(TEntity entity)
        {
            _profiler.Profile(
                () => _writerDelegate.Update(entity),
                $"function=[{MethodBase.GetCurrentMethod().Name}]; entity=[{_profiler.Serialize(entity)}]; "
            );
        }
    }
}

