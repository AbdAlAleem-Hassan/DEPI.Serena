
using Serena.DAL.Persistence.Repositories.Departments;

namespace Serena.DAL.Persistence.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
	public IDoctorRepository DoctorRepository { get; }
	public IServiceRepository ServiceRepository { get;  }
	public ILanguageRepository LanguageRepository { get;  }
	public IDoctorLanguageRepository DoctorLanguageRepository { get;  }
	public IPatientRepository PatientRepository { get; }
	public IDepartmentRepository DepartmentRepository { get; }
    Task<int> CompleteAsync();
}
