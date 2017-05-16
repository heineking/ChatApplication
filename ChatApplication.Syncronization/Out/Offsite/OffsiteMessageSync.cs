using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Syncronization.Out.Offsite
{
    public class OffsiteMessageSync : DataEventWriteSubscriber<MessageRecord>
    {
        public override void Saved(DataEvent<MessageRecord> dataEvent)
        {
            var actions = new List<Action>();
            CreatedEntities.ForEach(msg => actions.Add(new CreateMessage(msg).Execute));
            DeletedEntities.ForEach(msg => actions.Add(new DeleteMessage(msg).Execute));
            Parallel.ForEach(actions, a => a());
        }
    }
}
