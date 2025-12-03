using Serena.BLL.Models.Doctors;
using Serena.BLL.Models.Hospitals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.Home
{
    public class HomeIndexViewModel
    {
        public int TotalHospitals { get; set; }
        public int TotalDoctors { get; set; }
        public int TotalPatients { get; set; }
        public int TodayAppointments { get; set; }
        public int HealthScore { get; set; }
        public IEnumerable<HospitalDTO> TopHospitals { get; set; } = new List<HospitalDTO>();
        public IEnumerable<DoctorDTO> TopDoctors { get; set; } = new List<DoctorDTO>();
    }
}
