using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Data.Contracts.Models;

namespace ChatApplication.Data.EntityFramework.MappingConfig
{
    public class ClaimRecordMapping : EntityTypeConfiguration<ClaimRecord>
    {
        public ClaimRecordMapping()
        {
            HasKey(c => c.ClaimId);
            Property(l => l.ClaimName)
                .IsRequired();
        }
    }
}
