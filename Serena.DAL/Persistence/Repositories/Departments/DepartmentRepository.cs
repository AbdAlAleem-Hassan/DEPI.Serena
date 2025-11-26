namespace Serena.DAL.Persistence.Repositories.Departments
{
	public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
	{
		public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
		{
		}
	}
}
