using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Logging;
using ChatApplication.Logging.JsonNet;
using log4net;

namespace ChatApplication.Data.Contracts.Events
{
    public class DataEventWriteSubscriber<TEntity> : IEventSubscriber where TEntity : class
    {
        private readonly IDataEventWriterHandler<TEntity> _handler;
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        protected Dictionary<string, Action<TEntity>> StrategiesForSingleActions { get; set; }
        protected Dictionary<string, Action<IEnumerable<TEntity>>> StrategiesForBatchedActions { get; set; }

        public DataEventWriteSubscriber(IDataEventWriterHandler<TEntity> handler)
        {
            _handler = handler;
            StrategiesForSingleActions = new Dictionary<string, Action<TEntity>>
            {
                { EventName<TEntity>.Created, _handler.Add },
                { EventName<TEntity>.Deleted, _handler.Remove },
                { EventName<TEntity>.Updated, _handler.Update }
            };
            StrategiesForBatchedActions = new Dictionary<string, Action<IEnumerable<TEntity>>>
            {
                { EventName<TEntity>.CreatedMany, _handler.AddRange },
                { EventName<TEntity>.DeletedMany, _handler.RemoveRange },
            };
        }

        public virtual void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (StrategiesForSingleActions.ContainsKey(@event.Name))
            {
                var dataEvent = @event as DataEvent<TEntity>;
                if (dataEvent == null) return;
                StrategiesForSingleActions[dataEvent.Name](dataEvent.Entity);
            }
            if (StrategiesForBatchedActions.ContainsKey(@event.Name))
            {
                var dataEvent = @event as DataEvent<IEnumerable<TEntity>>;
                if (dataEvent == null) return;
                StrategiesForBatchedActions[dataEvent.Name](dataEvent.Entity);
            }
            if (@event.Name == EventName<TEntity>.Saved) _handler.Save();
        }
    }
}
