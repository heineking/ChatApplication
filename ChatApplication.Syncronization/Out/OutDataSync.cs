using System;
using System.Collections.Generic;
using ChatApplication.Infrastructure.Contracts.Events;

namespace ChatApplication.Syncronization.Out
{
    public class OutDataSync : IEventSubscriber
    {
        private readonly List<IEventSubscriber> _subscribers;

        public OutDataSync(List<IEventSubscriber> subscribers)
        {
            _subscribers = subscribers;
        }

        public void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            _subscribers.ForEach(s => s.Subscribe(@event));
        }
    }
}
