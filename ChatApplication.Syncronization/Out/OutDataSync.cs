using System;
using System.Collections.Generic;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Syncronization.Out.CRM;
using ChatApplication.Syncronization.Out.Offsite;

namespace ChatApplication.Syncronization.Out
{
    public class OutDataSync : IEventSubscriber
    {
        private readonly List<IEventSubscriber> _subscribers;

        public OutDataSync()
        {
            _subscribers = new List<IEventSubscriber>
            {
                new CrmMessageSync(),
                new OffsiteMessageSync()
            };
        }

        public void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            _subscribers.ForEach(s => s.Subscribe(@event));
        }
    }
}
