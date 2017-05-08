using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Infrastructure.Contracts;

namespace ChatApplication.Data.Contracts.Models
{
    public class UserRecord
    {
        public UserRecord() { }

        public UserRecord(string name)
        {
            Name = name;
        }
        [JsonLogging]
        public long UserId { get; set; }
        [JsonLogging]
        public string Name { get; set; }

        public virtual LoginRecord Login { get; set; }
        public virtual ICollection<MessageRecord> Messages { get; set; }
        public virtual ICollection<RoomRecord> Rooms { get; set; }
        public virtual ICollection<UserClaimsRecord> UserClaims { get; set; }
    }
}
