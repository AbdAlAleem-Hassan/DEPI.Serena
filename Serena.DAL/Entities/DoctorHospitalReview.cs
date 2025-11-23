using System.ComponentModel.DataAnnotations.Schema;

namespace Serena.DAL.Entities
{
    public class DoctorHospitalReview 
    {
        public int Rating { get; set; }
        public string? comment { get; set; }
        public DateTime? ReviewingDate { get; set; }


		// Navigation Properties
		[ForeignKey(nameof(Doctor))]
		public int? DoctorId { get; set; }
		public virtual Doctor? Doctor { get; set; }

		[ForeignKey(nameof(Hospital))]
		public int? HospitalId { get; set; }
		public virtual Hospital? Hospital { get; set; }
    }
}
