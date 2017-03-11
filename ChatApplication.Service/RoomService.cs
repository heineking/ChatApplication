using System;
using System.Collections.Generic;
using System.Linq;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Service.Contracts;

namespace ChatApplication.Service
{
    public class RoomService : IRoomReader, IRoomWriter
    {
        private readonly IUnitOfWork _uow;
        private readonly IModelMapper _mapper;

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

        public List<Message> GetRoomMessages(long roomId)
        {
            var messageRecords = _uow.Messages.Find(m => m.RoomId == roomId);
            return messageRecords.Select(_mapper.MessageRecordToMessage).ToList();
        }

        public void CreateRoom(Room room)
        {
            var roomRecord = _mapper.RoomToRoomRecord(room);
            _uow.Rooms.Add(roomRecord);
            _uow.SaveChanges();
        }

        public void AddMessage(Message message)
        {
            var messageRecord = _mapper.MessageToMessageRecord(message);
            _uow.Messages.Add(messageRecord);
            _uow.SaveChanges();
        }

        public List<Message> GetRoomMessagesFromDate(long roomId, long dateTime)
        {
            var messages = _uow.Messages.Find(m => m.RoomId == roomId && m.PostedDate > new DateTime(dateTime));
            return messages.Select(_mapper.MessageRecordToMessage).ToList();
        }
    }
}
