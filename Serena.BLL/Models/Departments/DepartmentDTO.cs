using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Serena.BLL.Models.Departments
{
	public class DepartmentDTO
	{
		public string Name { get; set; }

		public string HospitalName { get; set; }
		public int DoctorsNumber { get; set; }
	}
}
