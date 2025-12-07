    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace Serena.BLL.Models.Schedules
    {
        public class ScheduleDTO
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public decimal Price { get; set; }
            public bool IsAvailable { get; set; }
            public int DoctorId { get; set; }
            public string? DoctorName { get; set; }
        }
    }
