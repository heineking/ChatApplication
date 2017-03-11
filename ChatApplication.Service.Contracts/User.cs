using System;
using System.Collections.Generic;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Service.Contracts
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<Message> Messages { get; set; }
    }
}
