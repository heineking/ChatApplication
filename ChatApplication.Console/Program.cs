using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.Dapper.Repositories;
using ChatApplication.Data.DapperEF;
using ChatApplication.Data.DapperEF.Persistence;
using ChatApplication.Data.EntityFramework.ContextEF;
using ChatApplication.Data.EntityFramework.Persistence;
using ChatApplication.Infrastructure.Contracts;
using Newtonsoft.Json;

namespace ChatApplication.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var appSettings = new ConfigSettings();
            var connStr = appSettings.GetConnection("context");
            var context = new ChatContext(connStr);

            var efUow = new UnitOfWork(context);
            var roomReader = new RoomRepositoryReader(connStr);
            var roomRepository = new RoomRepositoryDecorator(roomReader, efUow.Rooms);
            var uow = new UnitOfWorkDecorated(efUow, roomRepository);
            uow.Rooms.Add(new RoomRecord("Music"));
            var rooms = uow.Rooms.GetAll();
            var beforeSave = JsonConvert.SerializeObject(rooms, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            uow.SaveChanges();
            var roomsUpdated = uow.Rooms.GetAll();
            var afterSave = JsonConvert.SerializeObject(roomsUpdated, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            System.Console.Write(beforeSave);
            System.Console.Write(afterSave);
            System.Console.Read();
        }
    }
}
