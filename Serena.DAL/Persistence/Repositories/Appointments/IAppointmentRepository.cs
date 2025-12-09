namespace Serena.DAL.Persistence.Repositories.Appointments;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    public Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
}