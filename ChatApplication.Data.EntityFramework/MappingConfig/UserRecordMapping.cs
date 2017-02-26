using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Data.EntityFramework.MappingConfig
{
    public class UserRecordMapping : EntityTypeConfiguration<UserRecord>
    {
        public UserRecordMapping()
        {
            HasKey(u => u.UserId);
        }
    }
}
