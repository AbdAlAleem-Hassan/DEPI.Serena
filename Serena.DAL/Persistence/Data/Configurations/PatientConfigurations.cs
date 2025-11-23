using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serena.DAL.Entities;
using System.Reflection.Emit;

namespace Serena.DAL.Persistence.Data.Configurations
{
	internal class PatientConfigurations : IEntityTypeConfiguration<Patient>
	{
		public void Configure(EntityTypeBuilder<Patient> builder)
		{
			builder.Property(d => d.Gender)
				   .HasConversion<string>()
				   .HasMaxLength(10);
		}
	}
}