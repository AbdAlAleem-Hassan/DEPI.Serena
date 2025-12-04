using Serena.BLL.Models.DoctorHospitalReviews;

namespace Serena.BLL.Services.DoctorHospitalReviews
{
    public interface IDoctorHospitalReviewService
    {
        Task<IEnumerable<DoctorHospitalReviewDTO>> GetAllAsync();
        Task<DoctorHospitalReviewDTO?> GetAsync(int doctorId, int hospitalId);
        Task<int> CreateAsync(DoctorHospitalReviewCreateUpdateDTO dto);
        Task<int> UpdateAsync(int doctorId, int hospitalId, DoctorHospitalReviewCreateUpdateDTO dto);
        Task<int> DeleteAsync(int doctorId, int hospitalId);
    }
}