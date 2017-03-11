using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Service.Contracts;

namespace ChatApplication.Infrastructure.Contracts
{
    public interface IModelMapper
    {
        MessageRecord MessageToMessageRecord(Message message);
        Message MessageRecordToMessage(MessageRecord messageRecord);

        RoomRecord RoomToRoomRecord(Room room);
        Room RoomRecordToRoom(RoomRecord roomRecord);

        UserRecord UserToUserRecord(User user);
        User UserRecordToUser(UserRecord userRecord);
    }
}
