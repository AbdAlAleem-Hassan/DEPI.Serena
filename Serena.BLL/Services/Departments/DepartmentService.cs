using Microsoft.EntityFrameworkCore;
using Serena.BLL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DepartmentDetailsDTO>> GetAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.DepartmentRepository.GetIQueryable()
                .Include(d => d.Hospital)
                .Select(d => new DepartmentDetailsDTO
                {
                    Name = d.Name,
                 
                })
                .AsNoTracking()
                .ToListAsync();

            return departments;
        }

        public async Task<DepartmentDetailsDTO?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
            if (department == null) return null;

            var dto = new DepartmentDetailsDTO
            {
                Name = department.Name,
                HospitalName = department.Hospital != null ? department.Hospital.Name : null
            };

            return dto;
        }

        public async Task<int> CreateDepartmentAsync(CreateAndUpdateDepartmentDTO dto)
        {
            var department = new DepartmentListDto
            {
                Name = dto.Name,
                HospitalId = dto.HospitalId
            };

            _unitOfWork.DepartmentRepository.Add(department);
            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> UpdateDepartmentAsync(int id, CreateAndUpdateDepartmentDTO dto)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
            if (department == null)
                throw new KeyNotFoundException("Department not found");

            // نفس ستايلك 1:1
            department.Name = dto.Name;
            department.HospitalId = dto.HospitalId;

            _unitOfWork.DepartmentRepository.Update(department);

            return await _unitOfWork.CompleteAsync();
        }

        public async Task<int> DeleteDepartmentAsync(int id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
            if (department == null) throw new KeyNotFoundException("Department not found");

            _unitOfWork.DepartmentRepository.Delete(department);
            return await _unitOfWork.CompleteAsync();
        }
    }
}   