using Serena.DAL.Entities;
using Serena.DAL.Persistence.Data;
using Serena.DAL.Persistence.Repositories._GenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.DoctorServices
{
	public class ServiceRepository : GenericRepository<Service>, IServiceRepository
	{
        public ServiceRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public IQueryable<Service> GetIQueryableWithDoctor()
        {
            return _dbContext.Set<Service>().Include(s => s.Doctor).Where(s => !s.IsDeleted);
        }
    }
}
