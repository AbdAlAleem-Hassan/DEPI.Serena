using System.ComponentModel.DataAnnotations.Schema;

namespace Serena.DAL.Entities
{
    public class PatientHospitalReview 
    {
        public int Rating { get; set; }
        public string? comment { get; set; }
        public DateTime? ReviewingDate { get; set; }
		
        // Navigation Properties
		[ForeignKey(nameof(Patient))]
		public int? PatientId { get; set; }
		public virtual Patient? Patient { get; set; }

		[ForeignKey(nameof(Hospital))]
		public int? HospitalId { get; set; }
		public virtual Hospital? Hospital { get; set; }
    }
}
