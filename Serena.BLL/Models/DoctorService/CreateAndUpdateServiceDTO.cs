using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Serena.BLL.Models.DoctorService
{
    public class CreateAndUpdateServiceDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        [Required]
        public int DoctorId { get; set; }
    }
}
