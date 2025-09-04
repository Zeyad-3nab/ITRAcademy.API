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
    public class QuestionConfigurations : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasOne(q=>q.exam)
                .WithMany()
                .HasForeignKey(q=>q.ExamId).OnDelete(DeleteBehavior.Cascade);


            builder.Property(e => e.Type).IsRequired();
            builder.Property(e => e.Content).IsRequired();
            builder.Property(e => e.ExamId).IsRequired();

        }
    }
}
