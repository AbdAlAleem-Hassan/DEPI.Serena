using Microsoft.EntityFrameworkCore;
using Serena.BLL.Models.Departments;

namespace Serena.BLL.Services.Departments
{
	public class DepartmentService : IDepartmentService
	{
		private readonly IUnitOfWork _unitOfWork;

		public DepartmentService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
		{
			var departmentsDto = await _unitOfWork.DepartmentRepository.GetIQueryable()
				.Select(d => new DepartmentDTO
				{
					Name = d.Name,
					HospitalName = d.Hospital.Name,
					DoctorsNumber = d.Doctors.ToList().Count
				}).AsNoTracking().ToListAsync();
			return departmentsDto;
		}

		public async Task<DepartmentDTO?> GetDepartmentByIdAsync(int doctorId)
		{
			var department = await _unitOfWork.DepartmentRepository.GetAsync(doctorId);
			if (department == null)
				return null;
			var departmentDetailsDto = new DepartmentDTO
			{
				Name = department.Name,
				HospitalName = department.Hospital.Name,
				DoctorsNumber = department.Doctors.ToList().Count
			};
			return departmentDetailsDto;
		}

		public async Task<int> CreateDepartmentAsync(CreateAndUpdateDepartmentDTO doctorDto)
		{
			var department = new Department
			{
				Name = doctorDto.Name
			};
			_unitOfWork.DepartmentRepository.Add(department);
			return await _unitOfWork.CompleteAsync();
		}

		public async Task<int> DeleteDepartmentAsync(int id)
		{
			var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
			if (department == null)
				return 0;
			_unitOfWork.DepartmentRepository.Delete(department);
			return await _unitOfWork.CompleteAsync();
		}


		public async Task<int> UpdateDepartmentAsync(int id, CreateAndUpdateDepartmentDTO doctorDto)
		{
			var department = await _unitOfWork.DepartmentRepository.GetAsync(id);
			if (department == null)
				return 0;
			department.Name = doctorDto.Name;
			_unitOfWork.DepartmentRepository.Update(department);
			return await _unitOfWork.CompleteAsync();
		}
	}
}
