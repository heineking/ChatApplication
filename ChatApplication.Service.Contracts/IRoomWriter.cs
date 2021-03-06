﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Service.Contracts
{
    public interface IRoomWriter
    {
        void CreateRoom(Room room);
        void AddMessage(Message message);
        void CreateRoom(string roomName, string description, long userId);
        void DeleteRoom(long roomId);
    }
}
