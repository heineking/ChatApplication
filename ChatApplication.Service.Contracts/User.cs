using System;
using System.Collections.Generic;
using ChatApplication.Data.Contracts.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ChatApplication.Service.Contracts
{
    public class User
    {
        public ObjectId _id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public List<Message> Messages { get; set; }
    }
}
