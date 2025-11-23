using System.ComponentModel.DataAnnotations.Schema;

namespace Serena.DAL.Entities
{
    public class PatientDoctorReview 
    {
        public int Rating { get; set; }
        public string? comment { get; set; }
        public DateTime? ReviewingDate { get; set; }
		// Navigation Properties
		
        [ForeignKey(nameof(Patient))]
		public int PatientId { get; set; }
		public virtual Patient Patient { get; set; }


		[ForeignKey(nameof(Doctor))]
		public int DoctorId { get; set; }
		public virtual Doctor Doctor { get; set; }
    }
}
