namespace Serena.DAL.Entities
{
	public class Language : ModelBase
	{
		public string LanguageName { get; set; }

		public virtual ICollection<DoctorLangauge>? DoctorLangauges { get; set; } = new HashSet<DoctorLangauge>();

	}
}