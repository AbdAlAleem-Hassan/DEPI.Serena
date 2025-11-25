using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serena.DAL.Entities;
using Serena.DAL.Persistence.Data;
using Serena.DAL.Persistence.Repositories._GenericRepository;

using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        

        public async Task<IEnumerable<Serena.DAL.Entities.Department>> GetAllAsync()
        {
            return await _context.Departments
                .Include(d => d.Doctors)

                .Include(d => d.Hospital)
                .ToListAsync();
        }

        public async Task<Serena.DAL.Entities.Department?> GetByIdAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.Doctors)
                .Include(d => d.Hospital)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Serena.DAL.Entities.Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
