
using Microsoft.EntityFrameworkCore.Migrations;
using Serena.DAL.Persistence.Repositories.Appointments;
using Serena.DAL.Persistence.Repositories.Departments;
using Serena.DAL.Persistence.Repositories.DoctorHospitalReviews;
using Serena.DAL.Persistence.Repositories.HospitalAddresses;
using Serena.DAL.Persistence.Repositories.Hospitals;
using Serena.DAL.Persistence.Repositories.PatientHospitalReviews;
using Serena.DAL.Persistence.Repositories.Schedules;

namespace Serena.DAL.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public IDoctorRepository DoctorRepository { get; private set; }
    public IServiceRepository ServiceRepository { get; private set; }
    public ILanguageRepository LanguageRepository { get; private set; }
    public IDoctorLanguageRepository DoctorLanguageRepository { get; private set; }
    public IPatientRepository PatientRepository { get; private set; }
    public IHospitalRepository HospitalRepository { get; private set; }
    public IHospitalAddressRepository HospitalAddressRepository { get; private set; }
    public IDepartmentRepository DepartmentRepository { get; private set; }

    public IAppointmentRepository AppointmentRepository { get; private set; }

    public IScheduleRepository ScheduleRepository { get; private set; }

    public IPatientHospitalReviewRepository PatientHospitalReviewRepository { get; private set; }
    public IDoctorHospitalReviewRepository DoctorHospitalReviewRepository { get; }

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        DoctorRepository = new DoctorRepository(_dbContext);
        ServiceRepository = new ServiceRepository(_dbContext);
        LanguageRepository = new LanguageRepository(_dbContext);
        DoctorLanguageRepository = new DoctorLanguageRepositity(_dbContext);
        PatientRepository = new PatientRepository(_dbContext);
        HospitalRepository = new HospitalRepository(_dbContext);
        HospitalAddressRepository = new HospitalAddressRepository(_dbContext);
        DepartmentRepository = new DepartmentRepository(_dbContext);
        AppointmentRepository = new AppointmentRepository(_dbContext);
        ScheduleRepository = new ScheduleRepository(_dbContext);
        PatientHospitalReviewRepository = new PatientHospitalReviewRepository(_dbContext);
        DoctorHospitalReviewRepository = new DoctorHospitalReviewRepository(_dbContext);

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

