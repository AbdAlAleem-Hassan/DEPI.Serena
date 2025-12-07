
namespace Serena.DAL.Persistence.Repositories.PatientsDoctorReviews;

public interface IPatientDoctorReviews 
{
    public Task CreateAsync(PatientDoctorReview patientDoctorReview);
    public Task<List<PatientDoctorReview>> GetAllAsync();
    public Task UpdateAsync(PatientDoctorReview patientDoctorReview);
    public Task DeleteAsync(int PatientId,int DoctorId);
    public Task<double?> GetAverageRatingForDoctorAsync(int doctorId);
    public Task<PatientDoctorReview?> GetAsync(int patientId, int doctorId);

}
