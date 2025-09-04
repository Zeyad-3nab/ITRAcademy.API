using ITR.API.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.BLL.Data.Configurations
{
    public class UserCourseConfigurations : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasOne(uc => uc.User)
                .WithMany()
                .HasForeignKey(uc => uc.UserId);

            builder.HasOne(uc => uc.Course)
                .WithMany()
                .HasForeignKey(uc => uc.CourseId);

            builder.HasKey(r => new { r.UserId, r.CourseId});

            builder.Property(uc=>uc.UserId).IsRequired();
            builder.Property(uc=>uc.CourseId).IsRequired();
            builder.Property(uc=>uc.State).IsRequired();
            builder.Property(uc=>uc.StartTime).IsRequired();
            builder.Property(uc=>uc.EndTime).IsRequired();

        }
    }
}
