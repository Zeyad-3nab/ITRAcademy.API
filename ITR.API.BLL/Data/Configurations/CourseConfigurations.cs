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
    public class CourseConfigurations : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Description).IsRequired();
            builder.Property(c => c.State).IsRequired();
            builder.Property(c => c.Type).IsRequired();
            //builder.Property(c => c.Price).IsRequired(false);
            builder.Property(c => c.ImageUrl).IsRequired();
        }
    }
}
