using Microsoft.AspNetCore.Http;
using Serena.BLL.Models.Addresses;
using Serena.BLL.Models.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Serena.BLL.Models.Hospitals
{
	public class CreateAndUpdateHospitalDTO
	{
		public string Name { get; set; }
		public string Rank { get; set; }
		[Column(TypeName = "decimal(18,2)")]
		public decimal AverageCost { get; set; }
		public string HospitalPhone { get; set; }
		public string EmergencyPhone { get; set; }

		public IFormFile Image { get; set; }
        public List<AddressDTO> Address { get; set; }
		public List<CreateAndUpdateDepartmentDTO> Department { get; set; }
    }
}
