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
    public class UserClaimRecordMapping : EntityTypeConfiguration<UserClaimsRecord>
    {
        public UserClaimRecordMapping()
        {
            HasKey(u => u.UserClaimsId);
            
            Property(u => u.UserClaimsId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            HasRequired(u => u.Claim)
                .WithMany(c => c.UserClaims)
                .HasForeignKey(u => u.ClaimId);

            HasRequired(u => u.User)
                .WithMany(u => u.UserClaims)
                .HasForeignKey(u => u.UserId);
        }
    }
}
