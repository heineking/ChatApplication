using System;
using System.Collections.Generic;

namespace ChatApplication.Infrastructure.Contracts.Events
{
    public class EventPublisher : IEventPublisher
    {
        private List<IEventSubscriber> Subscribers { get; }

        public EventPublisher()
        {
            Subscribers = new List<IEventSubscriber>();
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            Subscribers.ForEach(sub => sub.Subscribe(@event));
        }

        public void AddSubscriber(IEventSubscriber subscriber)
        {
            Subscribers.Add(subscriber);
        }
    }
}
