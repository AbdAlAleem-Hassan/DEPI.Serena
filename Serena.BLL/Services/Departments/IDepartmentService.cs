using Serena.BLL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Services.Departments
{
	public interface IDepartmentService
	{
		Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();
		Task<DepartmentDTO?> GetDepartmentByIdAsync(int doctorId);
		Task<int> CreateDepartmentAsync(CreateAndUpdateDepartmentDTO doctorDto);
		Task<int> UpdateDepartmentAsync(int id, CreateAndUpdateDepartmentDTO doctorDto);
		Task<int> DeleteDepartmentAsync(int id);
	}
}
