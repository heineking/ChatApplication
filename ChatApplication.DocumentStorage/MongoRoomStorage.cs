using System;
using System.Collections.Generic;
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

        public MongoRoomStorage(string connectionString)
        {
            _mongoConnectionString = connectionString;
        }

        public List<Room> GetAllRooms()
        {
            throw new NotImplementedException();
        }

        public List<Message> GetRoomMessages(long roomId)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetRoomMessagesFromDate(long roomId, long dateTime)
        {
            throw new NotImplementedException();
        }

        public void CreateRoom(Room room)
        {
            throw new NotImplementedException();
        }

        public void AddMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void CreateRoom(string roomName)
        {
            throw new NotImplementedException();
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
            var client = new MongoClient(connectionString);
            return client.GetDatabase(_chatDatabase);
        }
    }
}
