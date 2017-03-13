using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;
using ChatApplication.Data.EntityFramework.MappingConfig;

namespace ChatApplication.Data.EntityFramework.ContextEF
{
    public class ChatContext : DbContext
    {
        public ChatContext() : base("context") { }
        public ChatContext(string connectionStr) : base(connectionStr) { }

        public DbSet<UserRecord> Users { get; set; }
        public DbSet<RoomRecord> Rooms { get; set; }
        public DbSet<MessageRecord> Messages { get; set; }
        public DbSet<LoginRecord> Logins { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            builder.Configurations.Add(new UserRecordMapping());
            builder.Configurations.Add(new RoomRecordMapping());
            builder.Configurations.Add(new MessageRecordMapping());
            builder.Configurations.Add(new LoginRecordMapping());
        }
    }
}
