using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;

namespace ChatApplication.Data.DapperEF
{
    public class RoomRepositoryDecorator : IRoomRepository
    {
        private readonly IRepositoryReader<RoomRecord> _readerDelegate;
        private readonly IRepositoryWriter<RoomRecord> _writerDelegate;

        public RoomRepositoryDecorator(IRepositoryReader<RoomRecord> readerDelegate, IRepositoryWriter<RoomRecord> writerDelegate)
        {
            _readerDelegate = readerDelegate;
            _writerDelegate = writerDelegate;
        }
        public RoomRecord Get(int id)
        {
            return _readerDelegate.Get(id);
        }

        public IEnumerable<RoomRecord> GetAll()
        {
            return _readerDelegate.GetAll();
        }

        public IEnumerable<RoomRecord> Find(Expression<Func<RoomRecord, bool>> predicate)
        {
            return _readerDelegate.Find(predicate);
        }

        public void Add(RoomRecord entity)
        {
            _writerDelegate.Add(entity);
        }

        public void AddRange(IEnumerable<RoomRecord> entities)
        {
            _writerDelegate.AddRange(entities);
        }

        public void Remove(RoomRecord entity)
        {
            _writerDelegate.Remove(entity);
        }

        public void RemoveRange(IEnumerable<RoomRecord> entities)
        {
            _writerDelegate.RemoveRange(entities);
        }
    }
}
