namespace Serena.DAL.Persistence.Repositories.PatientHospitalReviews
{
    public interface IPatientHospitalReviewRepository
    {
        Task<List<PatientHospitalReview>> GetAllAsync();
        Task<PatientHospitalReview?> GetAsync(int patientId, int hospitalId);
        Task AddAsync(PatientHospitalReview review);
        void Update(PatientHospitalReview review);
        void Delete(PatientHospitalReview review);
    }
}