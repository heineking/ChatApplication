﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChatApplication.Service.Contracts;
using Nancy;

namespace ChatApplication.API.Modules
{
    public class RoomModule : NancyModule
    {
        public RoomModule(IRoomReader reader) : base("/api/v1/rooms")
        {
            Get["/all"] = _ => reader.GetAllRooms();
            Get["/{roomId:long}"] = p => reader.GetRoomMessages(p.roomId);
        }
    }
}