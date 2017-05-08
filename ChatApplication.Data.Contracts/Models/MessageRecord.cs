using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Infrastructure.Contracts;

namespace ChatApplication.Data.Contracts.Models
{
    public class MessageRecord
    {
        public MessageRecord() {}

        public MessageRecord(string text, long userId, long roomId)
        {
            Text = text;
            UserId = userId;
            RoomId = roomId;
        }
        [JsonLogging]
        public long MessageId { get; set; }
        [JsonLogging]
        public string Text { get; set; }
        [JsonLogging]
        public DateTime PostedDate { get; set; }
        
        [JsonLogging]
        public long RoomId { get; set; }
        [JsonLogging]
        public long UserId { get; set; }

        public virtual RoomRecord Room { get; set; }
        public virtual UserRecord User { get; set; }
    }
}
