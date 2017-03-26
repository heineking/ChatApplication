using System;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Service.Contracts
{
    public class Message
    {
        public long MessageId { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public DateTime PostedDate { get; set; }
        
        public long RoomId { get; set; }
        public Guid UserId { get; set; }
    }
}
