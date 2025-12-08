
namespace Serena.BLL.Services.PatientDoctorReviews;

public interface IPatientDoctorReviewService
{
    Task CreateAsync(PatientDoctorReviewCreateUpdateDTO dto);
    Task UpdateAsync(PatientDoctorReviewCreateUpdateDTO dto);
    Task DeleteAsync(int patientId, int doctorId);
    Task<List<PatientDoctorReviewGetDTO>> GetAllAsync();
    Task<double?> GetAverageRatingForDoctorAsync(int doctorId);
    Task<PatientDoctorReviewGetDTO?> GetAsync(int patientId, int doctorId);
}
