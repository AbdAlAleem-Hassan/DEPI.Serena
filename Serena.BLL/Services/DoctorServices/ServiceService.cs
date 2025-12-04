using Microsoft.EntityFrameworkCore;
using Serena.BLL.Models.DoctorService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Services.DoctorServices
{
    public class ServiceService : IServiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDTO>> GetAllServicesAsync()
        {
            var services = await _unitOfWork.ServiceRepository.GetIQueryableWithDoctor()
                .Include(s => s.Doctor) // لازم نضمّن Doctor لو هنستخدم اسمه
                .ToListAsync();

            return _mapper.Map<IEnumerable<ServiceDTO>>(services);
        }

        public async Task<ServiceDTO?> GetServiceByIdAsync(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetIQueryableWithDoctor()
                .Include(s => s.Doctor)
                .FirstOrDefaultAsync(s => s.Id == id);

            return service == null ? null : _mapper.Map<ServiceDTO>(service);
        }

        public async Task<int> CreateServiceAsync(CreateAndUpdateServiceDTO dto)
        {
            var service = _mapper.Map<Service>(dto);
            _unitOfWork.ServiceRepository.Add(service);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateServiceAsync(int id, CreateAndUpdateServiceDTO dto)
        {
            var service = await _unitOfWork.ServiceRepository.GetAsync(id);
            if (service == null) return 0;

            _mapper.Map(dto, service); // تحديث القيم من DTO للـ Entity
            _unitOfWork.ServiceRepository.Update(service);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteServiceAsync(int id)
        {
            var service = await _unitOfWork.ServiceRepository.GetAsync(id);
            if (service == null) return 0;

            _unitOfWork.ServiceRepository.Delete(service);
            return await _unitOfWork.CompleteAsync();
        }
    }
}
