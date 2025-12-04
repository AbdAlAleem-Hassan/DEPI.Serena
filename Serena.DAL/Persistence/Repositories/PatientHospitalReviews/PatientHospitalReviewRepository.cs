using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.PatientHospitalReviews
{
    public class PatientHospitalReviewRepository : IPatientHospitalReviewRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PatientHospitalReviewRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PatientHospitalReview>> GetAllAsync()
        {
            return await _dbContext.PatientHospitalReviews
                .Include(r => r.Patient)
                .Include(r => r.Hospital)
                .ToListAsync();
        }

        public async Task<PatientHospitalReview?> GetAsync(int patientId, int hospitalId)
        {
            return await _dbContext.PatientHospitalReviews
                .Include(r => r.Patient)
                .Include(r => r.Hospital)
                .FirstOrDefaultAsync(r =>
                    r.PatientId == patientId &&
                    r.HospitalId == hospitalId);
        }

        public async Task AddAsync(PatientHospitalReview review)
        {
            await _dbContext.PatientHospitalReviews.AddAsync(review);
        }

        public void Update(PatientHospitalReview review)
        {
            _dbContext.PatientHospitalReviews.Update(review);
        }

        public void Delete(PatientHospitalReview review)
        {
            _dbContext.PatientHospitalReviews.Remove(review);
        }
    }
}
