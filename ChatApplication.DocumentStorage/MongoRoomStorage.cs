using System;
using System.Collections.Generic;
using System.Linq;
using ChatApplication.Service.Contracts;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ChatApplication.DocumentStorage
{
    public class MongoRoomStorage : IRoomReader, IRoomWriter
    {
        private readonly string _mongoConnectionString;
        private readonly string _chatDatabase = "ChatApplication";
        private readonly string _roomsCollection = "rooms";

        public MongoRoomStorage()
        {
            _mongoConnectionString = "mongodb://host:27017/ChatApplication";
        }

        public List<Room> GetAllRooms()
        {
            var roomsCollection = GetRoomsCollection();
            return roomsCollection.Find(_ => true).ToList();
        }

        public List<Message> GetRoomMessages(long roomId)
        {
            var roomsCollection = GetRoomsCollection();
            var room = roomsCollection.Find(r => r.RoomId == roomId).FirstOrDefault();
            if (room == null) return new List<Message>();
            return room.Messages;
        }

        public List<Message> GetRoomMessagesFromDate(long roomId, long dateTime)
        {
            var roomsCollection = GetRoomsCollection();
            var room = roomsCollection.Find(r => r.RoomId == roomId).FirstOrDefault();
            if (room == null) return new List<Message>();
            return room.Messages.Where(m => m.PostedDate >= new DateTime(dateTime)).ToList();
        }

        public void CreateRoom(Room room)
        {
            var roomsCollection = GetRoomsCollection();
            roomsCollection.InsertOne(room);
        }

        public void AddMessage(Message message)
        {
            var roomsCollection = GetRoomsCollection();
            var room = roomsCollection.Find(r => r.RoomId == message.RoomId).FirstOrDefault();
            room.Messages.Add(message);
            roomsCollection.FindOneAndReplace(r => r.RoomId == message.RoomId, room);
        }

        private IMongoCollection<Room> GetRoomsCollection()
        {
            var database = GetDatabase();
            var roomsCollection = database.GetCollection<Room>(_roomsCollection);
            return roomsCollection;
        }

        private IMongoDatabase GetDatabase()
        {
            var connectionString = _mongoConnectionString;
            var client = new MongoClient();
            return client.GetDatabase(_chatDatabase);
        }

        public Room GetRoom(long roomId)
        {
            var rooms = GetRoomsCollection();
            return rooms.Find(r => r.RoomId == roomId).FirstOrDefault();
        }

        public void CreateRoom(string roomName, string description, long userId)
        {
            var rooms = GetRoomsCollection();
            long maxId = 0;
            var allRooms = rooms.Find(_ => true).ToList();
            if (allRooms.Any())
            {
                maxId = allRooms.Max(r => r.RoomId);
            }
            var room = new Room
            {
                RoomId = ++maxId,
                UserId = userId,
                Name = roomName,
                Description = description
            };
            rooms.InsertOne(room);
        }

        public void DeleteRoom(long roomId)
        {
            var rooms = GetRoomsCollection();
            rooms.DeleteOne(r => r.RoomId == roomId);
        }
    }
}
