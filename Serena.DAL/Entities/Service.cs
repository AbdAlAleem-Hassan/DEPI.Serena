using System.ComponentModel.DataAnnotations.Schema;

namespace Serena.DAL.Entities
{
    public class Service : ModelBase
    {
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        // Foreign Key
        [ForeignKey(nameof(Doctor))]
        public int? DoctorId { get; set; }
        // Navigation Property
        public virtual Doctor? Doctor { get; set; }
    }
}
