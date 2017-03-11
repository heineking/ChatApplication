﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Service.Contracts
{
    public interface IRoomReader
    {
        List<Room> GetAllRooms();
        List<Message> GetRoomMessages(long roomId);
    }
}