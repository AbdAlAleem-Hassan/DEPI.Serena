using Serena.DAL.Entities;
using Serena.DAL.Persistence.Data;
using Serena.DAL.Persistence.Repositories._GenericRepository;

namespace Serena.DAL.Persistence.Repositories.Doctors
{
	public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
	{
		public DoctorRepository(ApplicationDbContext dbContext) : base(dbContext)
		{

		}
	}
}
