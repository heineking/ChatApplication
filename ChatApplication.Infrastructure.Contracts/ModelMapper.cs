using System;
using System.Collections.Generic;
using System.Linq;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Security.Contracts;
using ChatApplication.Service.Contracts;

namespace ChatApplication.Infrastructure.Contracts
{
    public class ModelMapper : IModelMapper
    {
        public MessageRecord MessageToMessageRecord(Message message)
        {
            return new MessageRecord
            {
                Text = message.Text,
                PostedDate = new DateTime(message.PostedDate),
                UserId = message.UserId,
                RoomId = message.RoomId,
                MessageId = message.MessageId
            };
        }

        public Message MessageRecordToMessage(MessageRecord messageRecord)
        {
            return new Message
            {
                Text = messageRecord.Text,
                UserId = messageRecord.UserId,
                RoomId = messageRecord.RoomId,
                MessageId = messageRecord.MessageId,
                PostedDate = messageRecord.PostedDate.Ticks,
                UserName = messageRecord.User.Name
            };
        }

        public RoomRecord RoomToRoomRecord(Room room)
        {
            return new RoomRecord
            {
                Name = room.Name,
                Messages = room.Messages.Select(MessageToMessageRecord).ToList(),
                RoomId = room.RoomId
            };
        }

        public Room RoomRecordToRoom(RoomRecord roomRecord)
        {
            return new Room
            {
                Name = roomRecord.Name,
                Messages = roomRecord.Messages?.Select(MessageRecordToMessage).ToList() ?? new List<Message>(),
                RoomId = roomRecord.RoomId
            };
        }

        public UserRecord UserToUserRecord(User user)
        {
            return new UserRecord
            {
                Name = user.Name,
                Messages = user.Messages?.Select(MessageToMessageRecord).ToList(),
                UserId = user.UserId
            };
        }

        public User UserRecordToUser(UserRecord userRecord)
        {
            return new User
            {
                Name = userRecord.Name,
                Messages = userRecord.Messages?.Select(MessageRecordToMessage).ToList() ?? new List<Message>(),
                UserId = userRecord.UserId
            };
        }

        public LoginRecord LoginToLoginRecord(Login login)
        {
            return new LoginRecord
            {
                Username = login.LoginName,
                Password = login.Password,
                UserId = login.UserId
            };
        }

        public Login LoginRecordToLogin(LoginRecord loginRecord)
        {
            return new Login
            {
                LoginName = loginRecord.Username,
                UserId = loginRecord.UserId,
                Password = loginRecord.Password
            };
        }
    }
}
