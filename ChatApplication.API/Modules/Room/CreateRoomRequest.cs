using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatApplication.API.Modules.Room
{
    public class CreateRoomRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}