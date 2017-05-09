using System;
using System.Data;
using System.Reflection;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.EntityFramework.Events;
using ChatApplication.Logging.JsonNet;
using ChatApplication.Syncronization.Contracts.Commands;
using log4net;
using Newtonsoft.Json;

namespace ChatApplication.Syncronization.Archive
{
    public class Archive : IEventSubscriber
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event.Name == EntityFrameworkEvents.Delete(typeof(RoomRecord)))
            {
                Delete(@event);
            }
        }

        private void Delete<TEvent>(TEvent @event)
        {
            var deleteEvent = @event as EntityFrameworkDeleteEvent<RoomRecord>;
            if (deleteEvent?.Entity == null) return;
            var room = JsonConvert.SerializeObject(deleteEvent.Entity, new JsonSerializerSettings {
                ContractResolver = LoggingContractResolver<RoomRecord>.Instance()
            });
            _log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; type=[{nameof(RoomRecord)}]; room=[{room}]");
        }
    }
}
