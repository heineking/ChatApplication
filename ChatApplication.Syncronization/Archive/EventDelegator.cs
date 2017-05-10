using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Logging.JsonNet;
using Newtonsoft.Json;

namespace ChatApplication.Syncronization.Archive
{
    public abstract class EventDelegator
    {
        public void Delegator<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event.Name.Contains(nameof(RoomRecord))) RoomEvent(@event as DataEvent<RoomRecord>);
            if (@event.Name.Contains(nameof(MessageRecord))) MessageEvent(@event as DataEvent<MessageRecord>);
            if (@event.Name == "request/REQUEST_END") Finished();
        }
        
        private void RoomEvent(DataEvent<RoomRecord> roomEvent)
        {
            if (roomEvent?.Entity == null) return;

            if (roomEvent.Name == EventName.Delete(typeof(RoomRecord)))
                DeletedRoom(roomEvent);

            if (roomEvent.Name == EventName.Add(typeof(RoomRecord)))
                CreatedRoom(roomEvent);
        }

        public void MessageEvent(DataEvent<MessageRecord> messageEvent)
        {
            if (messageEvent?.Entity == null) return;

            if (messageEvent.Name == EventName.Add(typeof(MessageRecord)))
                CreatedMessage(messageEvent);

            if (messageEvent.Name == EventName.Delete(typeof(MessageRecord)))
                DeletedMessage(messageEvent);
        }

        public string SerializeEntity<TEntity>(DataEvent<TEntity> dataEvent) where TEntity : class
        {
            return JsonConvert.SerializeObject(dataEvent.Entity, new JsonSerializerSettings
            {
                ContractResolver = LoggingContractResolver<TEntity>.Instance()
            });
        }

        public virtual void CreatedRoom(DataEvent<RoomRecord> addedRoom) {}
        public virtual void DeletedRoom(DataEvent<RoomRecord> deletedRoom) {}

        public virtual void CreatedMessage(DataEvent<MessageRecord> messageRecord) {}
        public virtual void DeletedMessage(DataEvent<MessageRecord> messageRecord) {}

        public virtual void Finished() { }
    }
}
