using Serena.DAL.Common.Enums;

namespace Serena.DAL.Entities
{
    public class Patient : ModelBase
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string JobStatus { get; set; }
        public string InsuranceCompany { get; set; }
        public string NationalID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
		//Navigation Properties
		public virtual ICollection<PatientDoctorReview>? PatientDoctorReviews { get; set; } = new HashSet<PatientDoctorReview>();

		public ICollection<Appointment>? Appointments = new HashSet<Appointment>();

        public virtual ICollection<PatientHospitalReview>? PatientHospitalReviews { get; set; } = new HashSet<PatientHospitalReview>();
	}
}
