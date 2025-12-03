using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.DoctorHospitalReviews
{
    public class DoctorHospitalReviewRepository : IDoctorHospitalReviewRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorHospitalReviewRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DoctorHospitalReview>> GetAllAsync()
        {
            return await _dbContext.DoctorHospitalReviews
                .Include(r => r.Doctor)
                .Include(r => r.Hospital)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<DoctorHospitalReview?> GetAsync(int doctorId, int hospitalId)
        {
            return await _dbContext.DoctorHospitalReviews
                .Include(r => r.Doctor)
                .Include(r => r.Hospital)
                .FirstOrDefaultAsync(r =>
                    r.DoctorId == doctorId &&
                    r.HospitalId == hospitalId);
        }

        public async Task AddAsync(DoctorHospitalReview entity)
        {
            await _dbContext.DoctorHospitalReviews.AddAsync(entity);
        }

        public void Update(DoctorHospitalReview entity)
        {
            _dbContext.DoctorHospitalReviews.Update(entity);
        }

        public void Delete(DoctorHospitalReview entity)
        {
            _dbContext.DoctorHospitalReviews.Remove(entity);
        }
    }
}
