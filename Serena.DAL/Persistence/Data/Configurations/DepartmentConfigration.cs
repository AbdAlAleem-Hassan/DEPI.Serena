using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Data.Configurations
{
    public class DepartmentConfigration
    {
        public void Configure(EntityTypeBuilder<DepartmentListDto> builder)
        {
            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
