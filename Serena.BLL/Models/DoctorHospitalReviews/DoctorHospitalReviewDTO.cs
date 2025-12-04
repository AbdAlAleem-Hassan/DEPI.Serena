using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.DoctorHospitalReviews
{
    public class DoctorHospitalReviewDTO
    {
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewingDate { get; set; }

        public int? DoctorId { get; set; }
        public string? DoctorName { get; set; }

        public int? HospitalId { get; set; }
        public string? HospitalName { get; set; }
    }
}
