using Serena.BLL.Models.Addresses;
using Serena.BLL.Models.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Serena.BLL.Models.Hospitals
{
	public class HospitalDetailsDTO
	{
		public int Id { get; set; }
        public string Name { get; set; }
		public string Rank { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal AverageCost { get; set; }
		public string HospitalPhone { get; set; }
		public string EmergencyPhone { get; set; }
		public string ImageUrl { get; set; }

        public List<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();
		public List<DepartmentDTO> Departments { get; set; } = new List<DepartmentDTO>();
		public List<PatientHospitalReview>  PatientHospitalReviews { get; set; } = new List<PatientHospitalReview>();
		public List<DoctorHospitalReview>  DoctorHospitalReviews { get; set; } = new List<DoctorHospitalReview>();


	}
}
