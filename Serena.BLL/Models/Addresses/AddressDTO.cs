using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.Addresses
{
	public class AddressDTO
	{
		public string Street { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string Country { get; set; }
		public int ZipCode { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
