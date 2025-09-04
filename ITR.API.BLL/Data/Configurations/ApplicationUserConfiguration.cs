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
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            //builder.Property(u => u.Code)
            //       .UseIdentityColumn(1, 1)       // يبدأ من 1 ويزيد 1 كل مرة
            //       .ValueGeneratedOnAdd();
        }
    }
}