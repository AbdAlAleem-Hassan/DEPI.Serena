using Serena.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] 
namespace Serena.DAL.Entities
{
    public class Doctor : ModelBase
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
		public string? ImageUrl { get; set; }
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
        //Navigation Properties

        [ForeignKey(nameof( Department))]
		public int? DepartmentId { get; set; }
		[InverseProperty(nameof(Department.Doctors))]
		public virtual Department? Department { get; set; }

        [ForeignKey(nameof(Hospital))]
		public int? HospitalId { get; set; }
        [InverseProperty(nameof(Hospital.Doctors))]
		public virtual Hospital? Hospital { get; set; }

        public ICollection<Appointment>? Appointments = new HashSet<Appointment>();

        [ForeignKey(nameof(HospitalAddress))]
		public int? HospitalAddressId { get; set; }
        [InverseProperty(nameof(HospitalAddress.Doctors))]
		public virtual HospitalAddress? HospitalAddress { get; set; }

        public virtual ICollection<Schedule>? Schedules { get; set; } = new HashSet<Schedule>();

		public virtual ICollection<Service>? Services { get; set; } = new HashSet<Service>();
        public virtual ICollection<DoctorLangauge>? DoctorLangauges { get; set; } = new HashSet<DoctorLangauge>();
        public virtual ICollection<PatientDoctorReview>? PatientDoctorReviews { get; set; } = new HashSet<PatientDoctorReview>();

        public virtual ICollection<DoctorHospitalReview>? DoctorHospitalReviews { get; set; } = new HashSet<DoctorHospitalReview>();
	}
}
