using Serena.BLL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Serena.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDetailsDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDetailsDTO?> GetDepartmentByIdAsync(int id);
        Task<int> CreateDepartmentAsync(CreateAndUpdateDepartmentDTO dto);
        Task<int> UpdateDepartmentAsync(int Id, CreateAndUpdateDepartmentDTO dto);
        Task<int> DeleteDepartmentAsync(int id);
    } 
}
