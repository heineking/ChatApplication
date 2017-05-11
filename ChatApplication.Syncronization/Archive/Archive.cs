using System.Collections.Generic;
using System.Reflection;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Logging.JsonNet;
using log4net;
using Newtonsoft.Json;

namespace ChatApplication.Syncronization.Archive
{
    public class Archive : IEventSubscriber
    {
        private readonly List<IEventSubscriber> _subscribers;

        public Archive()
        {
            _subscribers = new List<IEventSubscriber>
            {
                new DataEventSubscriber<RoomRecord>(),
                new DataEventSubscriber<MessageRecord>(),
                new DataEventSubscriber<LoginRecord>()
            };
        }
        public void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            _subscribers.ForEach(s => s.Subscribe(@event));
        }
    }
}
