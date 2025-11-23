using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serena.DAL.Entities;

namespace Serena.DAL.Persistence.Data.Configurations
{
	internal class PatientHospitalReviewConfigurations : IEntityTypeConfiguration<PatientHospitalReview>
	{
		public void Configure(EntityTypeBuilder<PatientHospitalReview> builder)
		{
			builder.HasKey(phr => new { phr.PatientId, phr.HospitalId });
			
			builder.Property(phr => phr.ReviewingDate)
				.HasComputedColumnSql("GETDATE()");
		}
	}
}
