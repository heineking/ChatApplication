using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using ChatApplication.Logging;
using ChatApplication.Logging.JsonNet;
using ChatApplication.Logging.Profiling;
using log4net;

namespace ChatApplication.Data.Contracts.Repositories.Decorators
{
    public class RepositoryProfiler<TEntity> : IRepositoryReader<TEntity>, IRepositoryWriter<TEntity> where TEntity : class
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IRepositoryReader<TEntity> _readerDelegate;
        private readonly IRepositoryWriter<TEntity> _writerDelegate;
        private readonly IProfiler _profiler;

        public RepositoryProfiler(IRepositoryReader<TEntity> readerDelegate, IRepositoryWriter<TEntity> writerDelegate, IProfiler profiler)
        {
            _readerDelegate = readerDelegate;
            _writerDelegate = writerDelegate;
            _profiler = profiler;

            // set up profiler
            _profiler = profiler;
            _profiler.Log = _log;
            _profiler.Type = typeof(TEntity).Name;
        }
        public TEntity Get(long id)
        {
            var callerInfo = LoggingExtensions.Caller();
            return _profiler.Profile(
                () => _readerDelegate.Get(id),
                $"{callerInfo} - id=[{id}]"
            );
        }

        public IEnumerable<TEntity> GetAll()
        {
            var callerInfo = LoggingExtensions.Caller();
            return _profiler.Profile(
                () => _readerDelegate.GetAll(),
                callerInfo    
            );
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var callerInfo = LoggingExtensions.Caller();
            return _profiler.Profile(
                () => _readerDelegate.GetAll(),
                $"{callerInfo} - predicate=[{predicate}];"
            );
        }

        public void Add(TEntity entity)
        {
            var callerInfo = LoggingExtensions.Caller();
            _profiler.Profile(
                () => _writerDelegate.Add(entity),
                $"{callerInfo} - entity=[{JsonLogging.Serialize(entity)}];"
            );
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            var callerInfo = LoggingExtensions.Caller();
            _profiler.Profile(
                () => _writerDelegate.AddRange(entities),
                $"{callerInfo} - entities=[{JsonLogging.Serialize(entities)}];"
            );
        }

        public void Update(TEntity entity)
        {
            var callerInfo = LoggingExtensions.Caller();
            _profiler.Profile(
                () => _writerDelegate.Update(entity),
                $"{callerInfo} - entity=[{JsonLogging.Serialize(entity)}];"
            );
        }

        public void Remove(TEntity entity)
        {
            var callerInfo = LoggingExtensions.Caller();
            _profiler.Profile(
                () => _writerDelegate.Remove(entity),
                $"{callerInfo} - entity=[{JsonLogging.Serialize(entity)}];"
            );
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            var callerInfo = LoggingExtensions.Caller();
            _profiler.Profile(
                () => _writerDelegate.RemoveRange(entities),
                $"{callerInfo} - entities=[{JsonLogging.Serialize(entities)}];"
            );
        }
    }
}
