using Serena.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.Patients
{
    public class CreateAndUpdatePatientDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string JobStatus { get; set; }
        public string InsuranceCompany { get; set; }
        public string NationalID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

    }
}
