using System.ComponentModel.DataAnnotations.Schema;

namespace Serena.DAL.Entities
{
    public class Schedule : ModelBase
    {
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Doctor))]
		public int DoctorId { get; set; }
		public virtual Doctor? Doctor { get; set; }
	}
}
