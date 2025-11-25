using System.Collections.Generic;
using System.Threading.Tasks;
using Serena.DAL.Persistence.Data;
using Serena.DAL.Persistence.Repositories.Departments;
using Serena.DAL.Persistence.Repositories.DoctorLanguages;
using Serena.DAL.Persistence.Repositories.Doctors;
using Serena.DAL.Persistence.Repositories.DoctorServices;
using Serena.DAL.Persistence.Repositories.Languages;

namespace Serena.DAL.Persistence.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _dbContext;

		public IDoctorRepository DoctorRepository => new DoctorRepository(_dbContext);

		public IServiceRepository ServiceRepository => new ServiceRepository(_dbContext);

		public ILanguageRepository LanguageRepository => new LanguageRepository(_dbContext);

		public IDoctorLanguageRepository DoctorLanguageRepository => new DoctorLanguageRepositity(_dbContext);
		public IDepartmentRepository DepartmentRepository => new DepartmentRepository(_dbContext);

        public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<int> CompleteAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public ValueTask DisposeAsync()
		{
			return _dbContext.DisposeAsync();
		}

		
	}
}
