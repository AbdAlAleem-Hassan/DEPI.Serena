using Serena.BLL.Models.Appointements;

namespace Serena.BLL.Services.Appointments
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync();
        Task<AppointmentDTO?> GetAppointmentByIdAsync(int id);
        Task<int> CreateAppointmentAsync(CreateAndUpdateAppointmentDTO dto);
        Task<int> UpdateAppointmentAsync(int id, CreateAndUpdateAppointmentDTO dto);
        Task<int> DeleteAppointmentAsync(int id);
    }
}