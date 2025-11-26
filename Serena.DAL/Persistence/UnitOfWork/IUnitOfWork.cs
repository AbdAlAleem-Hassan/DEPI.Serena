
using Microsoft.EntityFrameworkCore.Migrations;
using Serena.DAL.Persistence.Repositories.Departments;
using Serena.DAL.Persistence.Repositories.HospitalAddresses;
using Serena.DAL.Persistence.Repositories.Hospitals;

namespace Serena.DAL.Persistence.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
	public IDoctorRepository DoctorRepository { get; }
	public IServiceRepository ServiceRepository { get;  }
	public ILanguageRepository LanguageRepository { get;  }
	public IDoctorLanguageRepository DoctorLanguageRepository { get;  }
	public IPatientRepository PatientRepository { get; }
	public IHospitalRepository	HospitalRepository { get; }
	public IHospitalAddressRepository HospitalAddressRepository { get; }
	public IDepartmentRepository DepartmentRepository { get; }

    Task<int> CompleteAsync();
}
