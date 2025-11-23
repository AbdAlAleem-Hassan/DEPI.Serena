using Serena.DAL.Entities;

namespace Serena.DAL.Persistence.Repositories._GenericRepository
{
	public interface IGenericRepository<T> where T : ModelBase 
	{
		Task<IEnumerable<T>> GetAllAsync(bool withAsNoTracking = true);
		IQueryable<T> GetIQueryable();
		Task<T?> GetAsync(int id);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);
	}
}
