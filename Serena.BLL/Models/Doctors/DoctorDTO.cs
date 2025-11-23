namespace Serena.BLL.Models.Doctors
{
	public class DoctorDTO
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string? Image { get; set; }
		public string Rank { get; set; }
		public int YearsOfExperience { get; set; }
		public string Department { get; set; }
		public string Hospital { get; set; }
		public string Specialization { get; set; }
	}
}
