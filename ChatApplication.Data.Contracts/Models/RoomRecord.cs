using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Infrastructure.Contracts;

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
        [JsonLogging]
        public long RoomId { get; set; }
        [JsonLogging]
        public long UserId { get; set; }
        [JsonLogging]
        public string Name { get; set; }
        [JsonLogging]
        public string Description { get; set; }
        [JsonLogging]
        public DateTime DateCreated { get; set; }
        public virtual ICollection<MessageRecord> Messages { get; set; }
        public virtual UserRecord User { get; set; }
    }
}
