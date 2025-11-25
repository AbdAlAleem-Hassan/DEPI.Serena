using Serena.BLL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.BLL.Services.Departments
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllAsync();
        Task<DepartmentDTO?> GetByIdAsync(int id);
        Task AddAsync(DepartmentDTO dto);
        Task UpdateAsync(DepartmentDTO dto);
        Task DeleteAsync(int id);
    }
}
