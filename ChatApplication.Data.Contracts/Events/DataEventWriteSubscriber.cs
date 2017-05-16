using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Logging;
using ChatApplication.Logging.JsonNet;
using log4net;

namespace ChatApplication.Data.Contracts.Events
{
    public class DataEventWriteSubscriber<TEntity> : IEventSubscriber where TEntity : class
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected Dictionary<string, Action<DataEvent<TEntity>>> StrategiesForSingleActions { get; set; }
        protected Dictionary<string, Action<DataEvent<IEnumerable<TEntity>>>> StrategiesForBatchedActions { get; set; }

        // cache of events
        protected List<TEntity> CreatedEntities { get; }
        protected List<TEntity> DeletedEntities { get; }
        protected List<TEntity> UpdatedEntities { get; }
        
        public DataEventWriteSubscriber()
        {
            StrategiesForSingleActions = new Dictionary<string, Action<DataEvent<TEntity>>>
            {
                { EventName<TEntity>.Created, Created },
                { EventName<TEntity>.Deleted, Deleted },
                { EventName<TEntity>.Updated, Updated },
                { EventName<TEntity>.Saved, Saved }
            };
            StrategiesForBatchedActions = new Dictionary<string, Action<DataEvent<IEnumerable<TEntity>>>>
            {
                { EventName<TEntity>.CreatedMany, CreatedMany },
                { EventName<TEntity>.DeletedMany, DeletedMany },

            };
            CreatedEntities = new List<TEntity>();
            DeletedEntities = new List<TEntity>();
            UpdatedEntities = new List<TEntity>();
        }

        public virtual void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (StrategiesForSingleActions.ContainsKey(@event.Name))
                StrategiesForSingleActions[@event.Name](@event as DataEvent<TEntity>);

            if (StrategiesForBatchedActions.ContainsKey(@event.Name))
                StrategiesForBatchedActions[@event.Name](@event as DataEvent<IEnumerable<TEntity>>);
        }

        public virtual void Created(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - {DataEventInformation(dataEvent)}");
            CreatedEntities.Add(dataEvent.Entity);
        }

        public virtual void CreatedMany(DataEvent<IEnumerable<TEntity>> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - {DataEventInformation(dataEvent)}");
            CreatedEntities.AddRange(dataEvent.Entity);
        }

        public virtual void Deleted(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - {DataEventInformation(dataEvent)}");
        }

        public virtual void DeletedMany(DataEvent<IEnumerable<TEntity>> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo}");
            DeletedEntities.AddRange(dataEvent.Entity);
        }

        public virtual void Updated(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - {DataEventInformation(dataEvent)}");
            UpdatedEntities.Add(dataEvent.Entity);
        }

        public virtual void Saved(DataEvent<TEntity> dataEvent)
        {

        }

        private string DataEventInformation(DataEvent<TEntity> dataEvent)
        {
            return $"entity=[{typeof(TEntity).Name}]; entity=[{JsonLogging.Serialize(dataEvent.Entity)}];";
        }

        private string DataEventInformation(DataEvent<IEnumerable<TEntity>> dataEvent)
        {
            return $"entity=[{typeof(TEntity).Name}]; entity=[{JsonLogging.Serialize(dataEvent.Entity)}];";
        }
    }
}
