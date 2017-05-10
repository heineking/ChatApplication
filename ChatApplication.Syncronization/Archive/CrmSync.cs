using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Infrastructure.Contracts.Events;
using ChatApplication.Syncronization.Commands;
using ChatApplication.Syncronization.Contracts.Commands;

namespace ChatApplication.Syncronization.Archive
{
    public class CrmSync : EventDelegator, IEventSubscriber
    {
        public override void CreatedMessage(DataEvent<MessageRecord> messageRecord)
        {
            var create = new CreateMessage(messageRecord.Entity);
            create.Execute();
        }

        public void Subscribe<TEvent>(TEvent @event) where TEvent : IEvent
        {
            Delegator(@event);
        }
    }
}
