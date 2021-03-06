﻿using System;
using System.Collections.Generic;
using System.Linq;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Mapper;
using ChatApplication.Service.Contracts;

namespace ChatApplication.Service
{
    public class RoomService : IRoomReader, IRoomWriter
    {
        private readonly IUnitOfWork _uow;
        private readonly IModelMapper _mapper;

        public void CreateRoom(Room room)
        {
            var roomRecord = _mapper.RoomToRoomRecord(room);
            _uow.Rooms.Add(roomRecord);
            _uow.SaveChanges();
        }

        public void CreateRoom(string roomName, string description, long userId)
        {
            var roomRecord = new RoomRecord(roomName, description, userId);
            _uow.Rooms.Add(roomRecord);
            _uow.SaveChanges();
        }

        public void DeleteRoom(long roomId)
        {
            var roomRecord = _uow.Rooms.Get(roomId);
            if (roomRecord == null) return;
            _uow.Rooms.Remove(roomRecord);
            _uow.SaveChanges();
        }

        public RoomService(IUnitOfWork uow, IModelMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public List<Room> GetAllRooms()
        {
            var roomRecords = _uow.Rooms.GetAll();
            return roomRecords.Select(_mapper.RoomRecordToRoom).ToList();
        }

        public Room GetRoom(long roomId)
        {
            var room = _uow.Rooms.Get(roomId);
            return _mapper.RoomRecordToRoom(room);
        }
        public List<Message> GetRoomMessages(long roomId)
        {
            var messageRecords = _uow.Messages.Find(m => m.RoomId == roomId);
            return messageRecords.Select(_mapper.MessageRecordToMessage).ToList();
        }

        public void AddMessage(Message message)
        {
            var messageRecord = _mapper.MessageToMessageRecord(message);
            _uow.Messages.Add(messageRecord);
            _uow.SaveChanges();
            message.MessageId = messageRecord.MessageId;
        }

        public List<Message> GetRoomMessagesFromDate(long roomId, long dateTime)
        {
            var messages = _uow.Messages.Find(m => m.RoomId == roomId && m.PostedDate > new DateTime(dateTime));
            return messages.Select(_mapper.MessageRecordToMessage).ToList();
        }
    }
}
