using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.PatientHospitalReviews
{
    public class CreateAndUpdatePatientHospitalReviewDTO
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int? PatientId { get; set; }
        public int? HospitalId { get; set; }
    }
}
