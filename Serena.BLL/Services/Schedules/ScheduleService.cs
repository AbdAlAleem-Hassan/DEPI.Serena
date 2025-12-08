using Microsoft.EntityFrameworkCore;
using Serena.BLL.Models.Schedules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Services.Schedules
{
    public class ScheduleService : IScheduleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync()
        {
            var schedules = await _unitOfWork.ScheduleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<ScheduleDTO>>(schedules);
        }

        public async Task<ScheduleDTO?> GetScheduleByIdAsync(int id)
        {
            var schedule = await _unitOfWork.ScheduleRepository.GetAsync(id);
            if (schedule == null) return null;

            return _mapper.Map<ScheduleDTO>(schedule);
        }

        public async Task<int> CreateScheduleAsync(CreateAndUpdateScheduleDTO dto)
        {
           var doctor = await _unitOfWork.DoctorRepository
                .GetIQueryable()
                .FirstOrDefaultAsync(d => d.UserId == dto.DoctorUserId);
            if (doctor == null) return 0;
            dto.DoctorId = doctor.Id;
            var schedule = _mapper.Map<Schedule>(dto);

            _unitOfWork.ScheduleRepository.Add(schedule);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateScheduleAsync(int id, CreateAndUpdateScheduleDTO dto)
        {
            var schedule = await _unitOfWork.ScheduleRepository.GetAsync(id);
            if (schedule == null) return 0;

            _mapper.Map(dto, schedule);
            _unitOfWork.ScheduleRepository.Update(schedule);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteScheduleAsync(int id)
        {
            var schedule = await _unitOfWork.ScheduleRepository.GetAsync(id);
            if (schedule == null) return 0;

            _unitOfWork.ScheduleRepository.Delete(schedule);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<IEnumerable<ScheduleDTO>> GetDoctorSchedulesAsync(string doctorUserId)
        {
            // جلب الدكتور من الـ UserId
            var doctor = await _unitOfWork.DoctorRepository
                .GetIQueryable()
                .FirstOrDefaultAsync(d => d.UserId == doctorUserId);

            if (doctor == null) return new List<ScheduleDTO>();

            // جلب المواعيد الخاصة بالدكتور
            var schedules = await _unitOfWork.ScheduleRepository
                .GetIQueryable()
                .Where(s => s.DoctorId == doctor.Id)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ScheduleDTO>>(schedules);
        }

    }
}

