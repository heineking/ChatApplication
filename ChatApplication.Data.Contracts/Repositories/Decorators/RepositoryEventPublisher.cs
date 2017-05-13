using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Infrastructure.Contracts.Events;

namespace ChatApplication.Data.Contracts.Repositories.Decorators
{
    public class RepositoryEventPublisher<TEntity> : IRepositoryWriter<TEntity>, IRepositoryReader<TEntity> where TEntity : class
    {
        private readonly IRepositoryReader<TEntity> _readerDelegate;
        private readonly IRepositoryWriter<TEntity> _writerDelegate;
        private readonly IEventPublisher _publisher;

        public RepositoryEventPublisher(IRepositoryReader<TEntity> readerDelegate, IRepositoryWriter<TEntity> writerDelegate, IEventPublisher publisher)
        {
            _readerDelegate = readerDelegate;
            _writerDelegate = writerDelegate;
            _publisher = publisher;
        }
        public TEntity Get(long id)
        {
            var entity = _readerDelegate.Get(id);
            _publisher.Publish(new DataEvent<TEntity>(entity, EventName<TEntity>.Read));
            return entity;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var entities = _readerDelegate.GetAll().ToArray();
            _publisher.Publish(new DataEvent<IEnumerable<TEntity>>(entities, EventName<TEntity>.ReadAll));
            return entities;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = _readerDelegate.Find(predicate).ToArray();
            _publisher.Publish(new DataEvent<IEnumerable<TEntity>>(entities, EventName<TEntity>.Find));
            return entities;
        }

        public void Add(TEntity entity)
        {
            _writerDelegate.Add(entity);
            _publisher.Publish(new DataEvent<TEntity>(entity, EventName<TEntity>.Created));
     
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            var entityArray = entities.ToArray();
            _writerDelegate.AddRange(entityArray);
            _publisher.Publish(new DataEvent<IEnumerable<TEntity>>(entityArray, EventName<TEntity>.CreatedMany));
        }

        public void Update(TEntity entity)
        {
            _writerDelegate.Update(entity);
            _publisher.Publish(new DataEvent<TEntity>(entity, EventName<TEntity>.Updated));
        }

        public void Remove(TEntity entity)
        {
            _writerDelegate.Remove(entity);
            _publisher.Publish(new DataEvent<TEntity>(entity, EventName<TEntity>.Deleted));
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            var entityArray = entities.ToArray();
            _writerDelegate.RemoveRange(entityArray);
            _publisher.Publish(new DataEvent<IEnumerable<TEntity>>(entityArray, EventName<TEntity>.DeletedMany));
        }
    }
}
