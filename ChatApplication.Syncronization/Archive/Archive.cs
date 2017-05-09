using System;
using System.Data;
using System.Data.Odbc;
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
            if (@event.Name.Contains(nameof(RoomRecord))) RoomEvent(@event);
            if (@event.Name.Contains(nameof(MessageRecord))) MessageEvent(@event);
        }

        #region Room Events
        private void RoomEvent<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var roomEvent = @event as EntityFrameworkModificationEvent<RoomRecord>;
            if (roomEvent?.Entity == null) return;

            if (@event.Name == EntityFrameworkEvents.Delete(typeof(RoomRecord)))
                DeletedRoom(roomEvent.Entity);

            if (@event.Name == EntityFrameworkEvents.Add(typeof(RoomRecord)))
                AddedRoom(roomEvent.Entity);
        }
        private void DeletedRoom(RoomRecord deletedRoom)
        {
            var room = JsonConvert.SerializeObject(deletedRoom, new JsonSerializerSettings
            {
                ContractResolver = LoggingContractResolver<RoomRecord>.Instance()
            });
            _log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; type=[{nameof(RoomRecord)}]; room=[{room}]");
        }
        private void AddedRoom(RoomRecord addedRoom)
        {
            var room = JsonConvert.SerializeObject(addedRoom, new JsonSerializerSettings
            {
                ContractResolver = LoggingContractResolver<RoomRecord>.Instance()
            });
            _log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; type=[{nameof(RoomRecord)}]; room=[{room}]");
        }
        #endregion

        #region Message Events
        public void MessageEvent<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var messageEvent = @event as EntityFrameworkModificationEvent<MessageRecord>;
            if (messageEvent?.Entity == null) return;
            if (messageEvent.Name == EntityFrameworkEvents.Add(typeof(MessageRecord)))
                AddMessage(messageEvent.Entity);
        }
        public void AddMessage(MessageRecord messageRecord)
        {
            var json = JsonConvert.SerializeObject(messageRecord, new JsonSerializerSettings
            {
                ContractResolver = LoggingContractResolver<MessageRecord>.Instance()
            });
            _log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; message=[{json}]; ");
        }
        #endregion
    }
}
