using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.Appointements
{
    public class AppointmentDTO
    {
        public int Id { get; set; }

        public int? DoctorId { get; set; }
        public string? DoctorName { get; set; }

        public int? PatientId { get; set; }
        public string? PatientName { get; set; }

        public int? ScheduleId { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }
    }
}
