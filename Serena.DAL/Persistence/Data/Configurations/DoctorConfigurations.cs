using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serena.DAL.Entities;
using System.Reflection.Emit;

namespace Serena.DAL.Persistence.Data.Configurations
{
	internal class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
	{
		public void Configure(EntityTypeBuilder<Doctor> builder)
		{
			builder.Property(d => d.Gender)
				   .HasConversion<string>()
				   .HasMaxLength(10);
            builder
               .HasOne(p => p.User)  
			   .WithMany()
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Cascade);
        }
	}
}
