using System;
using System.Collections.Generic;
using System.Reflection;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Logging;
using ChatApplication.Logging.JsonNet;
using log4net;

namespace ChatApplication.Data.Contracts.Events
{
    public class DataEventSubscriber<TEntity> : IEventSubscriber where TEntity : class
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected Dictionary<string, Action<DataEvent<TEntity>>> Strategies { get; set; }

        public DataEventSubscriber()
        {
            Strategies = new Dictionary<string, Action<DataEvent<TEntity>>>
            {
                { EventName<TEntity>.Created, Created },
                { EventName<TEntity>.CreatedMany, CreatedMany },
                { EventName<TEntity>.Deleted, Deleted },
                { EventName<TEntity>.DeletedMany, DeletedMany },
                { EventName<TEntity>.Read, Read },
                { EventName<TEntity>.Find, Find },
                { EventName<TEntity>.Updated, Updated },
                { EventName<TEntity>.Saved, Saved }
            };
        }

        public virtual void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (Strategies.ContainsKey(@event.Name)) Strategies[@event.Name](@event as DataEvent<TEntity>);
        }

        public virtual void Created(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - {DataEventInformation(dataEvent)}");
        }

        public virtual void CreatedMany(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - {DataEventInformation(dataEvent)}");
        }

        public virtual void Deleted(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - {DataEventInformation(dataEvent)}");
        }

        public virtual void DeletedMany(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - {DataEventInformation(dataEvent)}");
        }
        
        public virtual void Read(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - entity=[{nameof(TEntity)}];");
        }

        public virtual void Find(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - entity=[{nameof(TEntity)}];");
        }

        public virtual void Updated(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - {DataEventInformation(dataEvent)}");
        }

        public virtual void Saved(DataEvent<TEntity> dataEvent)
        {

        }

        private string DataEventInformation(DataEvent<TEntity> dataEvent)
        {
            return $"entity=[{nameof(TEntity)}]; entity=[{JsonLogging.Serialize(dataEvent.Entity)}];";
        }
    }
}
