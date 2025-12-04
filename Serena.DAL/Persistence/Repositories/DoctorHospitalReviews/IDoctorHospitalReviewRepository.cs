namespace Serena.DAL.Persistence.Repositories.DoctorHospitalReviews
{
    public interface IDoctorHospitalReviewRepository
    {
        Task<IEnumerable<DoctorHospitalReview>> GetAllAsync();
        Task<DoctorHospitalReview?> GetAsync(int doctorId, int hospitalId);
        Task AddAsync(DoctorHospitalReview entity);
        void Update(DoctorHospitalReview entity);
        void Delete(DoctorHospitalReview entity);
    }
}