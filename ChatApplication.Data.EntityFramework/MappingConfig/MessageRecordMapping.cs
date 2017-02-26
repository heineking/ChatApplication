using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Data.EntityFramework.MappingConfig
{
    public class MessageRecordMapping : EntityTypeConfiguration<MessageRecord>
    {
        public MessageRecordMapping()
        {
            HasKey(m => m.MessageId);
            Property(m => m.MessageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId);

            HasRequired(m => m.Room)
                .WithMany(r => r.Messages)
                .HasForeignKey(m => m.RoomId);
        }
    }
}
