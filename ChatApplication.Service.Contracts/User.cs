using System;
using System.Collections.Generic;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Service.Contracts
{
    public class User
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public List<Message> Messages { get; set; }
    }
}
