using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Contracts.Models
{
    public class UserRecord
    {
        public UserRecord() { }

        public UserRecord(string name)
        {
            Name = name;
        }
        public long UserId { get; set; }
        public string Name { get; set; }

        public virtual LoginRecord Login { get; set; }
        public virtual ICollection<MessageRecord> Messages { get; set; }
        public virtual ICollection<RoomRecord> Rooms { get; set; }
    }
}
