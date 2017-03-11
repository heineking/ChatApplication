using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Contracts.Repositories;
using ChatApplication.Data.Dapper.Repositories;
using ChatApplication.Data.EntityFramework.ContextEF;
using ChatApplication.Data.EntityFramework.Persistence;
using ChatApplication.Data.EntityFramework.Repositories;
using ChatApplication.Infrastructure.Contracts;
using Newtonsoft.Json;

namespace ChatApplication.Console
{
    class Program
    {
        static void Main(string[] args)
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

            var roomsUpdated = uow.Rooms.GetAll();
            var afterSave = JsonConvert.SerializeObject(roomsUpdated, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            System.Console.Write(afterSave);
            System.Console.Read();
        }
    }
}
