using Serena.BLL.Models.DoctorService;

namespace Serena.BLL.Services.DoctorServices
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceDTO>> GetAllServicesAsync();
        Task<ServiceDTO?> GetServiceByIdAsync(int id);
        Task<int> CreateServiceAsync(CreateAndUpdateServiceDTO dto);
        Task<int> UpdateServiceAsync(int id, CreateAndUpdateServiceDTO dto);
        Task<int> DeleteServiceAsync(int id);
    }
}