using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serena.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Data.Configurations
{
	internal class PatientDoctorReviewConfigurations : IEntityTypeConfiguration<PatientDoctorReview>
	{
		public void Configure(EntityTypeBuilder<PatientDoctorReview> builder)
		{
			builder.HasKey(pdr => new { pdr.PatientId, pdr.DoctorId });

			builder.Property(pdr => pdr.ReviewingDate)
				.HasComputedColumnSql("GETDATE()");

            builder.HasOne(pdr => pdr.Doctor)
                .WithMany(d => d.PatientDoctorReviews)
                .HasForeignKey(pdr => pdr.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pdr => pdr.Patient)
                .WithMany(p => p.PatientDoctorReviews)
                .HasForeignKey(pdr => pdr.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

        }
	}
}
