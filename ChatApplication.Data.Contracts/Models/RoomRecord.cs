using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Contracts.Models
{
    public class RoomRecord
    {
        public RoomRecord() {}

        public RoomRecord(string name)
        {
            Name = name;
            DateCreated = DateTime.Now;
        }
        public long RoomId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<MessageRecord> Messages { get; set; }
    }
}
