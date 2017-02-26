using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Contracts.Models
{
    public class RoomRecord
    {
        public RoomRecord() { }

        public RoomRecord(string name)
        {
            Name = name;
        }
        public long RoomId { get; set; }
        public string Name { get; private set; }

        public virtual ICollection<MessageRecord> Messages { get; set; }
    }
}
