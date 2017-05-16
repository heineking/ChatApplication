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
    public class DataEventReadSubscriber<TEntity> : IEventSubscriber where TEntity : class
    {
        protected readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected Dictionary<string, Action<DataEvent<TEntity>>> StrategiesForSingleEntity { get; set; }
        protected Dictionary<string, Action<DataEvent<IEnumerable<TEntity>>>> StrategiesForMultipleEntities { get; set; }

        public DataEventReadSubscriber()
        {
            StrategiesForSingleEntity = new Dictionary<string, Action<DataEvent<TEntity>>>
            {
                { EventName<TEntity>.Read, Read }
            };
            StrategiesForMultipleEntities = new Dictionary<string, Action<DataEvent<IEnumerable<TEntity>>>>
            {
                { EventName<TEntity>.ReadAll, ReadAll },
                { EventName<TEntity>.Find, Find }
            };
        }

        public virtual void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (StrategiesForSingleEntity.ContainsKey(@event.Name))
                StrategiesForSingleEntity[@event.Name](@event as DataEvent<TEntity>);
            if (StrategiesForMultipleEntities.ContainsKey(@event.Name))
                StrategiesForMultipleEntities[@event.Name](@event as DataEvent<IEnumerable<TEntity>>);
        }

        public virtual void Read(DataEvent<TEntity> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - entity=[{typeof(TEntity).Name}];");
        }

        public virtual void Find(DataEvent<IEnumerable<TEntity>> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - entity=[{typeof(TEntity).Name}];");
        }

        public virtual void ReadAll(DataEvent<IEnumerable<TEntity>> dataEvent)
        {
            var callerInfo = LoggingExtensions.Caller();
            Log.Info($"{callerInfo} - entity=[{typeof(TEntity).Name}];");
        }
    }
}
