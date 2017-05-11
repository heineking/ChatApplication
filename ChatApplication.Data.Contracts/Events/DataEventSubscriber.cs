using System;
using System.Collections.Generic;
using System.Reflection;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Logging.JsonNet;
using log4net;

namespace ChatApplication.Data.Contracts.Events
{
    public class DataEventSubscriber<TEntity> : IEventSubscriber where TEntity : class
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected Dictionary<string, Action<DataEvent<TEntity>>> Strategies { get; set; }
        protected List<DataEvent<TEntity>> Events { get; set; }

        public DataEventSubscriber()
        {
            Strategies = new Dictionary<string, Action<DataEvent<TEntity>>>
            {
                { EventName<TEntity>.Created, Created },
                { EventName<TEntity>.Delete, Deleted },
                { EventName<TEntity>.Read, Read },
                { EventName<TEntity>.Updated, Updated }
            };
            Events = new List<DataEvent<TEntity>>();
        }

        public void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event.Name == EventName<TEntity>.Saved) Execute();
            if (Strategies.ContainsKey(@event.Name)) Events.Add(@event as DataEvent<TEntity>);
        }
        
        public virtual void Created(DataEvent<TEntity> dataEvent)
        {
            Log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; entity=[{nameof(TEntity)}]; entity=[{JsonLogging.Serialize(dataEvent.Entity)}]");
        }

        public virtual void Deleted(DataEvent<TEntity> dataEvent)
        {
            Log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; entity=[{nameof(TEntity)}]; entity=[{JsonLogging.Serialize(dataEvent.Entity)}]");
        }

        public virtual void Read(DataEvent<TEntity> dataEvent)
        {
            Log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; entity=[{nameof(TEntity)}]; entity=[{JsonLogging.Serialize(dataEvent.Entity)}]");
        }

        public virtual void Updated(DataEvent<TEntity> dataEvent)
        {
            Log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; entity=[{nameof(TEntity)}]; entity=[{JsonLogging.Serialize(dataEvent.Entity)}]");
        }

        public virtual void Execute()
        {
            Events.ForEach(e => Strategies[e.Name](e));
        }
    }
}
