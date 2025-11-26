using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Models.PatientDoctorReviews
{
    public class PatientDoctorReviewGetDTO
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewingDate { get; set; }
    }

}
