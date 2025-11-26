using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.Hospitals
{
	public class HospitalRepository : GenericRepository<Hospital>, IHospitalRepository
	{
		public HospitalRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
