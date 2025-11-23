using Microsoft.EntityFrameworkCore;
using Serena.DAL.Entities;
using Serena.DAL.Persistence.Data;

namespace Serena.DAL.Persistence.Repositories._GenericRepository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
	{
		private readonly ApplicationDbContext _dbContext;

		public GenericRepository(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<T?> GetAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true)
		{
			if (withAsNoTracking)
				return await _dbContext.Set<T>().Where(e => !e.IsDeleted).AsNoTracking().ToListAsync();
			return await _dbContext.Set<T>().Where(e => !e.IsDeleted).ToListAsync();
		}
		public IQueryable<T> GetIQueryable() => _dbContext.Set<T>();
		public void Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
		}

		public void Update(T entity)
		{
			_dbContext.Set<T>().Update(entity);
		}  

		public void Delete(T entity)
		{
			entity.IsDeleted = true;
			_dbContext.Set<T>().Update(entity);
		}
		
	}
}
