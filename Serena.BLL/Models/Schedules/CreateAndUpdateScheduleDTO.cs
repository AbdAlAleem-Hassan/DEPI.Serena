using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Serena.BLL.Models.Schedules
{
    public class CreateAndUpdateScheduleDTO
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public int DoctorId { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string DoctorUserId { get; set; }
    }
}
