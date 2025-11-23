using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Serena.DAL.Entities
{
	public class DoctorLangauge : ModelBase
	{
		[ForeignKey(nameof(Doctor))]
		public int DoctorId { get; set; }
		public virtual Doctor? Doctor { get; set; }

		[ForeignKey(nameof(Language))]
		public int LanguageId { get; set; }
		public virtual Language? Language { get; set; }

		public string Level { get; set; }
	}
}
