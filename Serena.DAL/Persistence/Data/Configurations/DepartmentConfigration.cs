using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serena.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            
            builder.ToTable("Departments");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.IsDeleted)
                   .HasDefaultValue(false);

            // Hospital Relationship (Hospital 1 -> Many Departments)
            builder.HasOne(d => d.Hospital)
                   .WithMany(h => h.Departments)
                   .HasForeignKey(d => d.HospitalId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Doctor Relationship (Department 1 -> Many Doctors)
            builder.HasMany(d => d.Doctors)
                   .WithOne(doc => doc.Department)
                   .HasForeignKey(doc => doc.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
