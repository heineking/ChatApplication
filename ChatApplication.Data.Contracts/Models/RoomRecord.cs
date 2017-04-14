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

        public RoomRecord(string name, string description, long userId)
        {
            Name = name;
            UserId = userId;
            Description = description;
            DateCreated = DateTime.Now;
        }
        public long RoomId { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual ICollection<MessageRecord> Messages { get; set; }
        public virtual UserRecord User { get; set; }
    }
}
