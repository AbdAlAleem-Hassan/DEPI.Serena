using Serena.DAL.Entities;
using Serena.DAL.Persistence.Repositories._GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.Departments
{
    public interface IDepartmentRepository : IGenericRepository<Serena.DAL.Entities.Department>
    {
        Task<IEnumerable<Serena.DAL.Entities.Department>> GetAllAsync();
        Task<Serena.DAL.Entities.Department?> GetByIdAsync(int id);
        Task AddAsync(Serena.DAL.Entities.Department department);
        Task UpdateAsync(Serena.DAL.Entities.Department department);
        Task DeleteAsync(int id);
    }

}
