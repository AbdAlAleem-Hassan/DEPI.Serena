using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.Departments
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int? HospitalId { get; set; }
        public List<int>? DoctorIds { get; set; }
    }
}
