using Serena.BLL.Common.Services.Attachments;
using Serena.BLL.Models.Departments;
using Serena.DAL.Entities;
using Serena.DAL.Persistence.Repositories.Departments;
using Serena.DAL.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentRepository _repository;
        private readonly IAttachmentService _attachment;

        public DepartmentService(IUnitOfWork unitOfWork, IAttachmentService attachment)
        {
            _unitOfWork = unitOfWork; 
            _repository = unitOfWork.DepartmentRepository;
            _attachment = attachment;
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllAsync()
        {
            var departments = await _repository.GetAllAsync();
            return departments.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                Name = d.Name,
                HospitalId = d.HospitalId,
                DoctorIds = d.Doctors?.Select(doc => doc.Id).ToList()
            });
        }

        public async Task<DepartmentDTO?> GetByIdAsync(int id)
        {
            var d = await _repository.GetByIdAsync(id);
            if (d == null) return null;
            return new DepartmentDTO
            {
                Id = d.Id,
                Name = d.Name,
                HospitalId = d.HospitalId,
                DoctorIds = d.Doctors?.Select(doc => doc.Id).ToList()
            };
        }

        public async Task AddAsync(DepartmentDTO dto)
        {
            var department = new Department
            {
                Name = dto.Name,
                HospitalId = dto.HospitalId
            };
            await _repository.AddAsync(department);
        }

        public async Task UpdateAsync(DepartmentDTO dto)
        { 
            var department = new Department
            { 
                Id = dto.Id, 
                Name = dto.Name,
                HospitalId = dto.HospitalId
            };
            await _repository.UpdateAsync(department);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
