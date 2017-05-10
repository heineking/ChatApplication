using System;
using System.Collections.Generic;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Infrastructure.Contracts.Events;

namespace ChatApplication.Data.EntityFramework.Repositories
{
    public class RepositoryEventPublishing<TEntity> : IRepositoryWriter<TEntity> where TEntity : class
    {
        private readonly IRepositoryWriter<TEntity> _delegateWriter;
        private readonly IEventPublisher _publisher;

        public RepositoryEventPublishing(IRepositoryWriter<TEntity> delegateWriter, IEventPublisher publisher)
        {
            _delegateWriter = delegateWriter;
            _publisher = publisher;

        }
        public void Add(TEntity entity)
        {
            _delegateWriter.Add(entity);
            var entityEvent = new DataEvent<TEntity>(entity, EventName.Add(typeof(TEntity)));
            _publisher.Publish(entityEvent);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _delegateWriter.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            _delegateWriter.Remove(entity);
            var entityEvent = new DataEvent<TEntity>(entity, EventName.Delete(typeof(TEntity)));
            _publisher.Publish(entityEvent);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _delegateWriter.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            _delegateWriter.Update(entity);
        }
    }
}
