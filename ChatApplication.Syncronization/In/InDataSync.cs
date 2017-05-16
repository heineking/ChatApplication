using System;
using System.Collections.Generic;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Syncronization.In.CRM;

namespace ChatApplication.Syncronization.In
{
    public class InDataSync : IEventSubscriber
    {
        private readonly List<IEventSubscriber> _subscribers;

        public InDataSync()
        {
            _subscribers = new List<IEventSubscriber>
            {
                new CrmRoomSync()
            };
        }
        public void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            _subscribers.ForEach(s => s.Subscribe(@event));
        }
    }
}
