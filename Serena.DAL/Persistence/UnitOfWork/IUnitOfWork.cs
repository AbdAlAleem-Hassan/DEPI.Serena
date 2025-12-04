using Serena.DAL.Persistence.Repositories.Appointments;
using Serena.DAL.Persistence.Repositories.Departments;
using Serena.DAL.Persistence.Repositories.DoctorHospitalReviews;
using Serena.DAL.Persistence.Repositories.HospitalAddresses;
using Serena.DAL.Persistence.Repositories.Hospitals;
using Serena.DAL.Persistence.Repositories.PatientHospitalReviews;
using Serena.DAL.Persistence.Repositories.Schedules;

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

	public IAppointmentRepository AppointmentRepository { get; }
	public IScheduleRepository ScheduleRepository { get; }

	public IPatientHospitalReviewRepository PatientHospitalReviewRepository { get; }

    IDoctorHospitalReviewRepository DoctorHospitalReviewRepository { get; }

    Task<int> CompleteAsync();
}
