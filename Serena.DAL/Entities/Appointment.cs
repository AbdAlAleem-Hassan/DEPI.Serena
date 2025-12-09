using System.ComponentModel.DataAnnotations.Schema;

namespace Serena.DAL.Entities
{
    public class Appointment : ModelBase
    {
        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }
		public virtual Doctor? Doctor { get; set; }

		[ForeignKey(nameof(Patient))]
        public int? PatientId { get; set; }
        public virtual Patient? Patient { get; set; }
        [ForeignKey(nameof(Schedule))]
        public int ScheduleId { get; set; }
        public virtual Schedule? Schedule { get; set; }

    }
}
