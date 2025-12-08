
namespace Serena.DAL.Persistence.Repositories.PatientsDoctorReviews;

public class PatientDoctorReviewRepository : IPatientDoctorReviews
{
    private readonly ApplicationDbContext _dbContext;
    public PatientDoctorReviewRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task CreateAsync(PatientDoctorReview patientDoctorReview)
    {
        await _dbContext.PatientDoctorReviews.AddAsync(patientDoctorReview);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int PatientId,int DoctorId)
    {
        var review =
            await _dbContext.PatientDoctorReviews
            .FirstOrDefaultAsync(r => r.PatientId == PatientId && r.DoctorId == DoctorId);

       _dbContext.PatientDoctorReviews.Remove(review!);
       await _dbContext.SaveChangesAsync();
    }

    public async Task<List<PatientDoctorReview>> GetAllAsync()
    {
        var reviews = await _dbContext.PatientDoctorReviews.ToListAsync();
        return reviews;
    }

    public async Task<double?> GetAverageRatingForDoctorAsync(int doctorId)
    {
        var averageRating = await _dbContext.PatientDoctorReviews
            .Where(r => r.DoctorId == doctorId)
            .AverageAsync(r => (double?)r.Rating);
        return averageRating;
    }

    public async Task UpdateAsync(PatientDoctorReview patientDoctorReview)
    {
        _dbContext.PatientDoctorReviews.Update(patientDoctorReview);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PatientDoctorReview?> GetAsync(int patientId, int doctorId)
    {
        return await _dbContext.PatientDoctorReviews
            .FirstOrDefaultAsync(r => r.PatientId == patientId && r.DoctorId == doctorId);
    }
}
