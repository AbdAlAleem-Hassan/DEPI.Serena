using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Serena.BLL.Models.Hospitals
{
	public class HospitalDTO
	{
		public string Name { get; set; }
		public string Rank { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal AverageCost { get; set; }
		public string HospitalPhone { get; set; }
		public string EmergencyPhone { get; set; }
		public int DepartmentsNumber { get; set; }
		public List<string> Addresses { get; set; }

		public int PatientsReviews { get; set; }
		public int DoctorsReviews { get; set; }
	}
}
