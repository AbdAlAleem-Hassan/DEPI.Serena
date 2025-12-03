using Serena.BLL.Models.Schedules;

namespace Serena.BLL.Services.Schedules
{
    public interface IScheduleService
    {
        Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync();
        Task<ScheduleDTO?> GetScheduleByIdAsync(int id);
        Task<int> CreateScheduleAsync(CreateAndUpdateScheduleDTO dto);
        Task<int> UpdateScheduleAsync(int id, CreateAndUpdateScheduleDTO dto);
        Task<int> DeleteScheduleAsync(int id);
    }
}