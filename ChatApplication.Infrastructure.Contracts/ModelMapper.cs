using System;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Service.Contracts;

namespace ChatApplication.Infrastructure.Contracts
{
    public class ModelMapper : IModelMapper
    {
        public MessageRecord MessageToMessageRecord(Message message)
        {
            throw new NotImplementedException();
        }

        public Message MessageRecordToMessage(MessageRecord messageRecord)
        {
            throw new NotImplementedException();
        }

        public RoomRecord RoomToRoomRecord(Room room)
        {
            throw new NotImplementedException();
        }

        public Room RoomRecordToRoom(RoomRecord roomRecord)
        {
            throw new NotImplementedException();
        }

        public UserRecord UserToUserRecord(User user)
        {
            throw new NotImplementedException();
        }

        public User UserRecordToUser(UserRecord userRecord)
        {
            throw new NotImplementedException();
        }
    }
}
