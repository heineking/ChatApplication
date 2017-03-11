using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Contracts.Models
{
    public class MessageRecord
    {
        public MessageRecord() {}

        public MessageRecord(string text, Guid userId, long roomId)
        {
            Text = text;
            UserId = userId;
            RoomId = roomId;
        }
        public long MessageId { get; set; }
        public string Text { get; set; }
        
        public long RoomId { get; set; }
        public Guid UserId { get; set; }

        public virtual RoomRecord Room { get; set; }
        public virtual UserRecord User { get; set; }
    }
}
