using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Repositories;

namespace ChatApplication.Data.EntityFramework.Repositories
{
    public class RepositoryLogging<TEntity> : IRepositoryReader<TEntity>, IRepositoryWriter<TEntity> where TEntity : class
    {

        private readonly string _loggingFile = @"C:/temp/logging.txt";
        private readonly IRepositoryReader<TEntity> _readerDelegate;
        private readonly IRepositoryWriter<TEntity> _writerDelegate;

        public RepositoryLogging(IRepositoryReader<TEntity> readerDelegate, IRepositoryWriter<TEntity> writeDelegate)
        {
            _readerDelegate = readerDelegate;
            _writerDelegate = writeDelegate;
        }
        public TEntity Get(long id)
        {
            WriteLog($"Request to get entity of type {typeof(TEntity).Name} with id: {id}");
            return _readerDelegate.Get(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            WriteLog($"Request to get all entities of type {typeof(TEntity).Name}");
            return _readerDelegate.GetAll();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            WriteLog($"Request to get entity of type {typeof(TEntity).Name} by predicate: {predicate}");
            return _readerDelegate.Find(predicate);
        }

        public void Add(TEntity entity)
        {
            WriteLog($"Request to add entity of type {typeof(TEntity).Name}");
            _writerDelegate.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            WriteLog($"Request to add range of type {typeof(TEntity).Name}");
            _writerDelegate.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            WriteLog($"Request to remove entity of type {typeof(TEntity).Name}");
            _writerDelegate.Add(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            WriteLog($"Request to remove entities of type {typeof(TEntity).Name}");
        }

        private void WriteLog(string logText)
        {
            File.AppendAllText(_loggingFile, $"{DateTime.Now} | {logText}{Environment.NewLine}");
        }
    }
}
