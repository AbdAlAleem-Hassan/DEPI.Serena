using Serena.BLL.Models.Doctors;

namespace Serena.BLL.Services.Doctors
{
	public interface IDoctorService
	{
		Task<IEnumerable<DoctorDTO>> GetAllDoctorsAsync();
		Task<DoctorDetailsDTO?> GetDoctorByIdAsync(int doctorId);
		Task<int> CreateDoctorAsync(CreateAndUpdateDoctorDTO doctorDto);
		Task<int> UpdateDoctorAsync(CreateAndUpdateDoctorDTO doctorDto);
		Task<int> DeleteDoctorAsync(int id);
	}
}
