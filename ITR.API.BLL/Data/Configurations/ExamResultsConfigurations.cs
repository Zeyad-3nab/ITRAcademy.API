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
    public class ExamResultsConfigurations : IEntityTypeConfiguration<ExamResults>
    {
        public void Configure(EntityTypeBuilder<ExamResults> builder)
        {
            builder.HasKey(r => new { r.UserId, r.ExamId });


            builder.HasOne(er => er.User)
                .WithMany()
                .HasForeignKey(er => er.UserId);
        }
    }
}
