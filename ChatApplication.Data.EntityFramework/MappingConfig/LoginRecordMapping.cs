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
    public class LoginRecordMapping : EntityTypeConfiguration<LoginRecord>
    {
        public LoginRecordMapping()
        {
            // specify the UserId as the PK for Login
            HasKey(l => l.UserId);

            // set the login as unique across all rows
            Property(l => l.Login)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute()));

            // set the relationship up, this will require every login in to have a required user,
            // but every user might not necessarily have a login
            HasRequired(l => l.User)
                .WithOptional(u => u.Login);
        }
    }
}
