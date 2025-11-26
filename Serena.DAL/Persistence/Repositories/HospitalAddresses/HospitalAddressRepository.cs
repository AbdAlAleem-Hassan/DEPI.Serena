using System;
using System.Collections.Generic;
using System.Text;

namespace Serena.DAL.Persistence.Repositories.HospitalAddresses
{
	public class HospitalAddressRepository : GenericRepository<HospitalAddress>, IHospitalAddressRepository
	{
		public HospitalAddressRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
