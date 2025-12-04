using Microsoft.EntityFrameworkCore;
using Serena.BLL.Models.Appointements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Services.Appointments
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync()
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetIQueryable()
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();

            return _mapper.Map<IEnumerable<AppointmentDTO>>(appointments);
        }

        public async Task<AppointmentDTO?> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAsync(id);
            if (appointment == null) return null;

            return _mapper.Map<AppointmentDTO>(appointment);
        }

        public async Task<int> CreateAppointmentAsync(CreateAndUpdateAppointmentDTO dto)
        {
            var appointment = _mapper.Map<Appointment>(dto);
            _unitOfWork.AppointmentRepository.Add(appointment);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateAppointmentAsync(int id, CreateAndUpdateAppointmentDTO dto)
        {
            var existing = await _unitOfWork.AppointmentRepository.GetAsync(id);
            if (existing == null) return 0;

            _mapper.Map(dto, existing);
            _unitOfWork.AppointmentRepository.Update(existing);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteAppointmentAsync(int id)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAsync(id);
            if (appointment == null) return 0;

            _unitOfWork.AppointmentRepository.Delete(appointment);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
