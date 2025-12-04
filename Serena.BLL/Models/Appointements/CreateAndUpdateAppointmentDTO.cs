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
        public DateTime Date { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal to 0")]
        public decimal Price { get; set; }
    }
}
