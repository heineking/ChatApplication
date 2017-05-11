using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Infrastructure.Contracts.Events;

namespace ChatApplication.Syncronization.Archive
{
    public class CrmSync : IEventSubscriber
    {
        private readonly List<IEventSubscriber> _subscribers;

        public CrmSync()
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
