
using Microsoft.EntityFrameworkCore.Migrations;
using Serena.DAL.Persistence.Repositories.Departments;
using Serena.DAL.Persistence.Repositories.HospitalAddresses;
using Serena.DAL.Persistence.Repositories.Hospitals;

namespace Serena.DAL.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
	private readonly ApplicationDbContext _dbContext;

	public IDoctorRepository DoctorRepository => new DoctorRepository(_dbContext);

	public IServiceRepository ServiceRepository => new ServiceRepository(_dbContext);

	public ILanguageRepository LanguageRepository => new LanguageRepository(_dbContext);

	public IDoctorLanguageRepository DoctorLanguageRepository => new DoctorLanguageRepositity(_dbContext);

	public IPatientRepository PatientRepository => new PatientRepository(_dbContext);

	public IHospitalRepository HospitalRepository => new HospitalRepository(_dbContext);

	public IHospitalAddressRepository HospitalAddressRepository => new HospitalAddressRepository(_dbContext);

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
