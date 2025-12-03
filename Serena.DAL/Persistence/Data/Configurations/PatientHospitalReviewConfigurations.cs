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

            builder.HasOne(phr => phr.Hospital)
                .WithMany(h => h.PatientHospitalReviews)
                .HasForeignKey(phr => phr.HospitalId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(phr => phr.Patient)
                .WithMany(p => p.PatientHospitalReviews)
                .HasForeignKey(phr => phr.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
	}
}
