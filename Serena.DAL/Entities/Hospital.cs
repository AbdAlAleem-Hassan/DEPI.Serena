using System.ComponentModel.DataAnnotations.Schema;

namespace Serena.DAL.Entities
{
    public class Hospital : ModelBase
    {
        public string Name { get; set; }
        public string Rank { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AverageCost { get; set; }
        public string HospitalPhone { get; set; }
        public string EmergencyPhone { get; set; }

        //navigation properties
        public virtual ICollection<DepartmentListDto>? Departments { get; set; } = new HashSet<DepartmentListDto>();
		public virtual ICollection<Doctor>? Doctors { get; set; } = new HashSet<Doctor>();

        public virtual ICollection<HospitalAddress>? HospitalAddresses { get; set; } = new HashSet<HospitalAddress>();
        public virtual ICollection<PatientHospitalReview>? PatientHospitalReviews { get; set; } = new HashSet<PatientHospitalReview>();
		public virtual ICollection<DoctorHospitalReview>? DoctorHospitalReviews { get; set; } = new HashSet<DoctorHospitalReview>();

	}
}
