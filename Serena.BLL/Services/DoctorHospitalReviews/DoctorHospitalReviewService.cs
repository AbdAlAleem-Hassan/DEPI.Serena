using Serena.BLL.Models.DoctorHospitalReviews;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Services.DoctorHospitalReviews
{
    public class DoctorHospitalReviewService : IDoctorHospitalReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DoctorHospitalReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DoctorHospitalReviewDTO>> GetAllAsync()
        {
            var reviews = await _unitOfWork.DoctorHospitalReviewRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DoctorHospitalReviewDTO>>(reviews);
        }

        public async Task<DoctorHospitalReviewDTO?> GetAsync(int doctorId, int hospitalId)
        {
            var review = await _unitOfWork.DoctorHospitalReviewRepository.GetAsync(doctorId, hospitalId);
            return _mapper.Map<DoctorHospitalReviewDTO>(review);
        }

        public async Task<int> CreateAsync(DoctorHospitalReviewCreateUpdateDTO dto)
        {
            var entity = _mapper.Map<DoctorHospitalReview>(dto);
            entity.ReviewingDate = DateTime.UtcNow;

            await _unitOfWork.DoctorHospitalReviewRepository.AddAsync(entity);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateAsync(int doctorId, int hospitalId, DoctorHospitalReviewCreateUpdateDTO dto)
        {
            var review = await _unitOfWork.DoctorHospitalReviewRepository.GetAsync(doctorId, hospitalId);
            if (review == null) return 0;

            _mapper.Map(dto, review);
            review.ReviewingDate = DateTime.UtcNow;

            _unitOfWork.DoctorHospitalReviewRepository.Update(review);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteAsync(int doctorId, int hospitalId)
        {
            var review = await _unitOfWork.DoctorHospitalReviewRepository.GetAsync(doctorId, hospitalId);
            if (review == null) return 0;

            _unitOfWork.DoctorHospitalReviewRepository.Delete(review);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
