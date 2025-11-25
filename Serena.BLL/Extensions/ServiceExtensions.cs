using Microsoft.Extensions.DependencyInjection;
using Serena.BLL.Helper;
using Serena.BLL.Services.Doctors;
using Serena.BLL.Services.PatientDoctorReviews;
using Serena.BLL.Services.Patients;

namespace Serena.BLL.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientDoctorReviewService, PatientDoctorReviewService>();

            return services;
        }
    }
}
