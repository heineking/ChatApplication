using System.Reflection;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Logging.JsonNet;
using log4net;
using Newtonsoft.Json;

namespace ChatApplication.Syncronization.Archive
{
    public class Archive : EventDelegator, IEventSubscriber
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            Delegator(@event);
        }

        public override void CreatedRoom(DataEvent<RoomRecord> addedRoom)
        {
            var room = SerializeEntity(addedRoom);
            _log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; type=[{nameof(RoomRecord)}]; room=[{room}];");
        }
        public override void DeletedRoom(DataEvent<RoomRecord> deletedRoom)
        {
            var room = SerializeEntity(deletedRoom);
            _log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; type=[{nameof(RoomRecord)}]; room=[{room}]");
        }
        public override void CreatedMessage(DataEvent<MessageRecord> messageRecord)
        {
            var json = SerializeEntity(messageRecord);
            _log.Info($"function=[{MethodBase.GetCurrentMethod().Name}]; message=[{json}]; ");
        }

        public override void DeletedMessage(DataEvent<MessageRecord> messageRecord)
        {
            throw new System.NotImplementedException();
        }
        
    }
}
