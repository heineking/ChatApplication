using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Service.Contracts
{
    public class Room
    {
        public long RoomId { get; set; }
        public string Name { get; set; }

        public List<MessageRecord> Messages { get; set; }
    }
}
