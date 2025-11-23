using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Serena.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Data.Configurations
{
	public class DoctorLanguageConfigurations : IEntityTypeConfiguration<DoctorLangauge>
	{
		public void Configure(EntityTypeBuilder<DoctorLangauge> builder)
		{
			builder.HasKey(dl => new { dl.DoctorId, dl.LanguageId });
		}
	}
}
