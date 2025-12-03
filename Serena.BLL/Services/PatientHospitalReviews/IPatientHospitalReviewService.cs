using Serena.BLL.Models.PatientHospitalReviews;

namespace Serena.BLL.Services.PatientHospitalReviews
{
    public interface IPatientHospitalReviewService
    {
        Task<IEnumerable<PatientHospitalReviewDTO>> GetAllAsync();
        Task<PatientHospitalReviewDTO?> GetAsync(int patientId, int hospitalId);

        Task<int> CreateAsync(CreateAndUpdatePatientHospitalReviewDTO dto);
        Task<int> UpdateAsync(int patientId, int hospitalId, CreateAndUpdatePatientHospitalReviewDTO dto);
        Task<int> DeleteAsync(int patientId, int hospitalId);

    }
}