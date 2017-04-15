using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApplication.Service.Contracts
{
    public class Room
    {
        public Room()
        {
            Messages = new List<Message>();
        }

        public ObjectId _id { get; set; }
        public long RoomId { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public int MessageCount => Messages?.Count ?? 0;
        public DateTime DateCreated { get; set; }

        public List<Message> Messages { get; set; }
    }
}
