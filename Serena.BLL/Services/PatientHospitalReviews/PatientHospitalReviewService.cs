using Serena.BLL.Models.PatientHospitalReviews;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Services.PatientHospitalReviews
{
    public class PatientHospitalReviewService : IPatientHospitalReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientHospitalReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientHospitalReviewDTO>> GetAllAsync()
        {
            var reviews = await _unitOfWork.PatientHospitalReviewRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PatientHospitalReviewDTO>>(reviews);
        }

        public async Task<PatientHospitalReviewDTO?> GetAsync(int patientId, int hospitalId)
        {
            var review = await _unitOfWork.PatientHospitalReviewRepository.GetAsync(patientId, hospitalId);
            return _mapper.Map<PatientHospitalReviewDTO>(review);
        }

        public async Task<int> CreateAsync(CreateAndUpdatePatientHospitalReviewDTO dto)
        {
            var review = _mapper.Map<PatientHospitalReview>(dto);
            review.ReviewingDate = DateTime.UtcNow;

            await _unitOfWork.PatientHospitalReviewRepository.AddAsync(review);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateAsync(int patientId, int hospitalId, CreateAndUpdatePatientHospitalReviewDTO dto)
        {
            var review = await _unitOfWork.PatientHospitalReviewRepository.GetAsync(patientId, hospitalId);
            if (review == null) return 0;

            _mapper.Map(dto, review);

            _unitOfWork.PatientHospitalReviewRepository.Update(review);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteAsync(int patientId, int hospitalId)
        {
            var review = await _unitOfWork.PatientHospitalReviewRepository.GetAsync(patientId, hospitalId);
            if (review == null) return 0;

            _unitOfWork.PatientHospitalReviewRepository.Delete(review);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
