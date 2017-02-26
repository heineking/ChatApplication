using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var context = new ChatContext(appSettings.GetConnection("context"));
            var init = new ChatInitializer();
            init.InitializeDatabase(context);
            var uow = new UnitOfWork(context);
            var messages = uow.Messages.GetAll().ToList();
            var json = JsonConvert.SerializeObject(messages, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            System.Console.Write(json);
            System.Console.Read();
        }
    }
}
