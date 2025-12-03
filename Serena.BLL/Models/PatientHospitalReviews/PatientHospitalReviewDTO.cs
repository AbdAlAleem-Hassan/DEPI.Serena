using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.PatientHospitalReviews
{
    public class PatientHospitalReviewDTO
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewingDate { get; set; }

        public int? PatientId { get; set; }
        public string? PatientName { get; set; }

        public int? HospitalId { get; set; }
        public string? HospitalName { get; set; }
    }
}
