using System.ComponentModel.DataAnnotations.Schema;

namespace Serena.DAL.Entities
{
    public class HospitalAddress : ModelBase
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [ForeignKey(nameof(Hospital))]
        public int? HospitalId { get; set; }
        public virtual Hospital? Hospital { get; set; }
        public virtual ICollection<Doctor>? Doctors { get; set; } = new HashSet<Doctor>();
    }
}
