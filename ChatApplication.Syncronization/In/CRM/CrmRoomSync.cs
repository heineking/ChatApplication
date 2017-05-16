using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Events;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Syncronization.In.CRM.Room;

namespace ChatApplication.Syncronization.In.CRM
{
    public class CrmRoomSync : DataEventReadSubscriber<RoomRecord>
    {
        public override void ReadAll(DataEvent<IEnumerable<RoomRecord>> dataEvent)
        {
            base.ReadAll(dataEvent);
            var commands = dataEvent.Entity.Select(roomRecord => (Action) new MergeRoom(roomRecord).Execute).ToList();
            Parallel.ForEach(commands, command => command());
        }
    }
}
