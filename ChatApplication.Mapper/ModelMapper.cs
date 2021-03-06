﻿using System;
using System.Collections.Generic;
using System.Linq;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Security.Contracts;
using ChatApplication.Service.Contracts;

namespace ChatApplication.Mapper
{
    public class ModelMapper : IModelMapper
    {
        public MessageRecord MessageToMessageRecord(Message message)
        {
            return new MessageRecord
            {
                Text = message.Text,
                PostedDate = message.PostedDate,
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
                PostedDate = messageRecord.PostedDate,
                UserName = messageRecord.User.Name
            };
        }

        public RoomRecord RoomToRoomRecord(Room room)
        {
            return new RoomRecord
            {
                Name = room.Name,
                Description = room.Description,
                DateCreated = room.DateCreated,
                Messages = room.Messages.Select(MessageToMessageRecord).ToList(),
                RoomId = room.RoomId,
                UserId = room.UserId
            };
        }

        public Room RoomRecordToRoom(RoomRecord roomRecord)
        {
            return new Room
            {
                Name = roomRecord.Name,
                DateCreated = roomRecord.DateCreated,
                Description = roomRecord.Description,
                Messages = roomRecord.Messages?.Select(MessageRecordToMessage).ToList() ?? new List<Message>(),
                RoomId = roomRecord.RoomId,
                UserId = roomRecord.UserId,
                UserName = roomRecord.User.Name
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
                IsAdmin = userRecord.UserClaims.FirstOrDefault(uc => uc.Claim.ClaimName == "admin") != null,
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
