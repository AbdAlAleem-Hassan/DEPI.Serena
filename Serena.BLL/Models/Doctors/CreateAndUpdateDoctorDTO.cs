using Microsoft.AspNetCore.Http;
using Serena.DAL.Common.Enums;
using Serena.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.Doctors
{
	public class CreateAndUpdateDoctorDTO
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int PhoneNumber { get; set; }
		public Gender Gender { get; set; }
		public DateTime DateOfBirth { get; set; }
		public IFormFile? Image { get; set; }
		public string MaritalStatus { get; set; }
		public string Specialization { get; set; }
		public string SubSpecialization { get; set; }
		public string Rank { get; set; }
		public string LicenseNumber { get; set; }
		public int YearsOfExperience { get; set; }
		public string NationalID { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string Country { get; set; }
		public int ZipCode { get; set; }

		public int? DepartmentId { get; set; }
		public int? HospitalId { get; set; }
		public int? HospitalAddressId { get; set; }

		public List<Service>? Services { get; set; }
		public List<int>? LanguageIds { get; set; }



	}
}
