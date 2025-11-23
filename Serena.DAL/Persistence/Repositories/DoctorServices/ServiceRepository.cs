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
		public ServiceRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
