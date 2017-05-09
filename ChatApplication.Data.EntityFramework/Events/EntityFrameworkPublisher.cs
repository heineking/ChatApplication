using System;
using System.Collections.Generic;
using ChatApplication.Data.Contracts.Events;

namespace ChatApplication.Data.EntityFramework.Events
{
    public class EntityFrameworkPublisher : IEventPublisher
    {
        private List<IEventSubscriber> Subscribers { get; }

        public EntityFrameworkPublisher()
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
