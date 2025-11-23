using System.ComponentModel.DataAnnotations.Schema;

namespace Serena.DAL.Entities
{
    public class Department : ModelBase
    {
        public string Name { get; set; }

        //navigation properties
        [ForeignKey(nameof(Hospital))]
        public int? HospitalId { get; set; }
        public virtual Hospital? Hospital { get; set; }
        public virtual ICollection<Doctor>? Doctors { get; set; } = new HashSet<Doctor>();

	}
}
