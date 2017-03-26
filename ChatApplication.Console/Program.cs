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
            var loginReader = CreateUnitOfWork();
            var logins = loginReader.GetAll().Select(l => new {l.Username, l.UserId, l.Password}).ToList();
            var afterSave = JsonConvert.SerializeObject(logins, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            System.Console.Write(afterSave);
            System.Console.Read();
        }

        public static IRepositoryReader<LoginRecord> CreateUnitOfWork()
        {
            // set up the UnitOfWork
            var appSettings = new ConfigSettings();
            var connStr = appSettings.GetConnection("context");
            var context = new ChatContext(connStr);
            return new RepositoryEF<LoginRecord>(context);
        }

        //public static IRoomReader GetRoomReaderService()
        //{
        //    var uow = CreateUnitOfWork();
        //    return new RoomService(uow, new ModelMapper());
        //}
    }
}
