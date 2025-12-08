using Serena.BLL.Models.Addresses;
using Serena.BLL.Models.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Serena.BLL.Models.Hospitals
{
	public class HospitalDTO
	{
		public int Id { get; set; }
        public string Name { get; set; }
		public string Rank { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal AverageCost { get; set; }
        public string? ImageUrl { get; set; }
        public string HospitalPhone { get; set; }
		public string EmergencyPhone { get; set; }
		public List<DepartmentDTO> Departments { get; set; }= new List<DepartmentDTO>();

		public List<AddressDTO> Address { get; set; }= new List<AddressDTO>();

        public int PatientsReviews { get; set; }
		public int DoctorsReviews { get; set; }

	}
}
