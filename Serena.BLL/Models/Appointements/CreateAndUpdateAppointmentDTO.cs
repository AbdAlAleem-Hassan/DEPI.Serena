using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Serena.BLL.Models.Appointements
{
    public class CreateAndUpdateAppointmentDTO
    {
        [Required]
        public int? DoctorId { get; set; }

        [Required]
        public int? PatientId { get; set; }

        [Required]
        public int? ScheduleId { get; set; }
    }
}
