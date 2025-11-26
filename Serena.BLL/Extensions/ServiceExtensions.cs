using Microsoft.Extensions.DependencyInjection;
using Serena.BLL.Common.Services.Attachments;
using Serena.BLL.Helper;
using Serena.BLL.Services.Departments;
using Serena.BLL.Services.Doctors;
using Serena.BLL.Services.Hospitals;
using Serena.BLL.Services.PatientDoctorReviews;
using Serena.BLL.Services.Patients;
using Serena.DAL.Persistence.Repositories.Departments;
using Serena.DAL.Persistence.Repositories.Doctors;
using Serena.DAL.Persistence.Repositories.Hospitals;
using Serena.DAL.Persistence.Repositories.Patients;

namespace Serena.BLL.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IDoctorRepository, DoctorRepository>();
			services.AddScoped<IPatientRepository, PatientRepository>();
			services.AddScoped<IPatientDoctorReviews, PatientDoctorReviewRepository>();
			services.AddScoped<IHospitalRepository, HospitalRepository>();
			services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			
			services.AddScoped<IDoctorService, DoctorService>();
			services.AddScoped<IPatientService, PatientService>();
			services.AddScoped<IPatientDoctorReviewService, PatientDoctorReviewService>();
			services.AddScoped<IHospitalService, HospitalService>();
			services.AddScoped<IDepartmentService, DepartmentService>();
			services.AddScoped<IAttachmentService, AttachmentService>();

			return services;
        }
    }
}
