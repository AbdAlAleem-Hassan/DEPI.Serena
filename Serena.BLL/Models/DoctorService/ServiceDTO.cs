using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.DoctorService
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public int? DoctorId { get; set; }
        public string? DoctorName { get; set; }
    }
}
