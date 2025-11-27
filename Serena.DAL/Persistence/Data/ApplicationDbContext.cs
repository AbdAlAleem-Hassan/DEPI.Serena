using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Serena.DAL.Entities;
using System.Reflection;

namespace Serena.DAL.Persistence.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server= .; Database = DEPI_PROJECT; Trusted_Connection=True; TrustServerCertificate = True;")
				.UseLazyLoadingProxies();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<DepartmentListDto> Departments { get; set; }
		public DbSet<Hospital> Hospitals { get; set; }
		public DbSet<HospitalAddress> HospitalAddresses { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<PatientDoctorReview> PatientDoctorReviews { get; set; }
		public DbSet<PatientHospitalReview> PatientHospitalReviews { get; set; }
		public DbSet<DoctorHospitalReview> DoctorHospitalReviews { get; set; }
		public DbSet<DoctorLangauge> DoctorLangauges { get; set; }
		public DbSet<ContactUs> contactUs { get; set; }
	}
}
