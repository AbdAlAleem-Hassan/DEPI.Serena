
namespace Serena.BLL.Services.PatientDoctorReviews;

public class PatientDoctorReviewService : IPatientDoctorReviewService
{
    private readonly IPatientDoctorReviews _reviewRepo;
    private readonly IMapper _mapper;

    public PatientDoctorReviewService(IPatientDoctorReviews reviewRepo, IMapper mapper)
    {
        _reviewRepo = reviewRepo;
        _mapper = mapper;
    }

    public async Task CreateAsync(PatientDoctorReviewCreateUpdateDTO dto)
    {
        var entity = _mapper.Map<PatientDoctorReview>(dto);
        entity.ReviewingDate = DateTime.UtcNow;

        await _reviewRepo.CreateAsync(entity);
    }

    public async Task UpdateAsync(PatientDoctorReviewCreateUpdateDTO dto)
    {
        var entity = _mapper.Map<PatientDoctorReview>(dto);
        entity.ReviewingDate = DateTime.UtcNow;

        await _reviewRepo.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int patientId, int doctorId)
    {
        await _reviewRepo.DeleteAsync(patientId, doctorId);
    }

    public async Task<List<PatientDoctorReviewGetDTO>> GetAllAsync()
    {
        var reviews = await _reviewRepo.GetAllAsync();
        return _mapper.Map<List<PatientDoctorReviewGetDTO>>(reviews);
    }

    public async Task<double?> GetAverageRatingForDoctorAsync(int doctorId)
    {
        return await _reviewRepo.GetAverageRatingForDoctorAsync(doctorId);
    }
}
