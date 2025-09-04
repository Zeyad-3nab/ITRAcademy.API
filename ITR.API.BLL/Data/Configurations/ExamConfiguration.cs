using ITR.API.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITR.API.BLL.Data.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasOne(e=>e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.SetNull);


            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Duration).IsRequired();
            builder.Property(e => e.EndTime).IsRequired();
            builder.Property(e => e.StartTime).IsRequired();
            builder.Property(e => e.State).IsRequired();
            builder.Property(e => e.QuestionDegree).IsRequired();

        }
    }
}
