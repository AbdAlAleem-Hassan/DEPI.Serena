using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.Appointments
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {

            var appointments = await _context.Appointments
                .Where(a => a.PatientId == patientId) 
                .Include(a => a.Doctor)   
                .Include(a => a.Schedule) 
                .ToListAsync();

            return appointments;
        }
    }
}
