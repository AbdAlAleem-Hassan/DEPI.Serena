using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serena.DAL.Entities;
using System.Reflection.Emit;

namespace Serena.DAL.Persistence.Data.Configurations
{
	internal class DoctorHospitalReviewConfigurations : IEntityTypeConfiguration<DoctorHospitalReview>
	{
		public void Configure(EntityTypeBuilder<DoctorHospitalReview> builder)
		{
			builder.HasKey(dhr => new { dhr.DoctorId, dhr.HospitalId });

			builder.Property(dhr => dhr.ReviewingDate)
					.HasComputedColumnSql("GETDATE()");

			builder.HasOne(dhr => dhr.Doctor)
					.WithMany(d => d.DoctorHospitalReviews)
					.HasForeignKey(dhr => dhr.DoctorId)
					.OnDelete(DeleteBehavior.Cascade);



			builder.HasOne(dhr => dhr.Hospital)
					.WithMany(h => h.DoctorHospitalReviews)
					.HasForeignKey(dhr => dhr.HospitalId)
					.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
