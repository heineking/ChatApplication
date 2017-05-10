using ChatApplication.Infrastructure.Contracts.Events;

namespace ChatApplication.Data.Contracts.Events
{
    public class DataEvent<TEntity> : IEvent where TEntity : class
    {
        public TEntity Entity { get; }
        public string Name { get; }

        public DataEvent(TEntity entity, string eventName)
        {
            Entity = entity;
            Name = eventName;
        }
    }
}
