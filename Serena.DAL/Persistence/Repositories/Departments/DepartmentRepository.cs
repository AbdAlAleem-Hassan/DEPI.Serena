using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<DepartmentListDto>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
