using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Persistence;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.Dapper.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;
using ChatApplication.Data.EntityFramework.Persistence;
using ChatApplication.Data.EntityFramework.Repositories;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Service;
using ChatApplication.Service.Contracts;
using Newtonsoft.Json;

namespace ChatApplication.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var roomService = GetRoomReaderService();
            var rooms = roomService.GetAllRooms();
            var afterSave = JsonConvert.SerializeObject(rooms, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            System.Console.Write(afterSave);
            System.Console.Read();
        }

        public static IUnitOfWork CreateUnitOfWork()
        {
            // set up the UnitOfWork
            var appSettings = new ConfigSettings();
            var connStr = appSettings.GetConnection("context");
            var context = new ChatContext(connStr);
            var roomReader = new RoomRepositoryReader(connStr);
            var rooms = new RoomRepository(roomReader, new RepositoryEF<RoomRecord>(context));
            var users = new UserRepository(new RepositoryEF<UserRecord>(context), new RepositoryEF<UserRecord>(context));
            var messages = new MessageRepository(new RepositoryEF<MessageRecord>(context), new RepositoryEF<MessageRecord>(context));
            var uow = new EntityFrameworkUnitOfWork(context, users, rooms, messages);
            return uow;
        }

        public static IRoomReader GetRoomReaderService()
        {
            var uow = CreateUnitOfWork();
            return new RoomService(uow, new ModelMapper());
        }
    }
}
