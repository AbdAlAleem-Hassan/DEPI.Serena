using Serena.BLL.Models.Doctors;
using Serena.BLL.Models.Hospitals;

namespace Serena.BLL.Models.Home
{
    public class SearchResultsViewModel
    {
        public string Query { get; set; } = string.Empty;
        public string Type { get; set; } = "all";
        public IEnumerable<HospitalDTO> Hospitals { get; set; } = new List<HospitalDTO>();
        public IEnumerable<DoctorDTO> Doctors { get; set; } = new List<DoctorDTO>();
        public int TotalResults => Hospitals.Count() + Doctors.Count();
    }
}
