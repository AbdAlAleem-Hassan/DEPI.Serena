using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serena.BLL.Services.Doctors;
using Serena.BLL.Services.Hospitals;
using Serena.BLL.Services.Patients;
using Serena.BLL.Services.Departments;
using Serena.BLL.Services.DoctorServices;
using Serena.BLL.Services.Schedules;
using Serena.DAL.Entities;

namespace Serena.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IHospitalService _hospitalService;
        private readonly IDepartmentService _departmentService;
        private readonly IServiceService _serviceService;
        private readonly IScheduleService _scheduleService;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            IDoctorService doctorService,
            IPatientService patientService,
            IHospitalService hospitalService,
            IDepartmentService departmentService,
            IServiceService serviceService,
            IScheduleService scheduleService)
        {
            _userManager = userManager;
            _doctorService = doctorService;
            _patientService = patientService;
            _hospitalService = hospitalService;
            _departmentService = departmentService;
            _serviceService = serviceService;
            _scheduleService = scheduleService;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            var patients = await _patientService.GetAllPatientsAsync();
            var hospitals = await _hospitalService.GetAllHospitalsAsync();

            // Assuming 'New Requests' acts as checking recently created users or pending ones
            // For now, let's count users created in the last 7 days
            var recentDate = DateTime.UtcNow.AddDays(-7);
            var newRequestsCount = await _userManager.Users.CountAsync(u => u.CreatedAt >= recentDate);

            ViewData["DoctorsCount"] = doctors.Count();
            ViewData["PatientsCount"] = patients.Count();
            ViewData["HospitalsCount"] = hospitals.Count();
            ViewData["NewRequestsCount"] = newRequestsCount;

            return View();
        }

        public async Task<IActionResult> Doctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return View(doctors);
        }

        public async Task<IActionResult> Patients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return View(patients);
        }

        public async Task<IActionResult> Hospitals()
        {
            var hospitals = await _hospitalService.GetAllHospitalsAsync();
            return View(hospitals);
        }

        public async Task<IActionResult> NewRequests()
        {
            var recentDate = DateTime.UtcNow.AddDays(-30); // Last 30 days for list
            var newUsers = await _userManager.Users
                .Where(u => u.CreatedAt >= recentDate)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            // To show roles, we might need a ViewModel, but for now we can pass the list of ApplicationUser
            // Or better, a ViewModel that includes Roles.

            return View(newUsers);
        }

        public async Task<IActionResult> Departments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }

        public async Task<IActionResult> Services()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return View(services);
        }

        public async Task<IActionResult> Schedules()
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return View(schedules);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.IsActive = true;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction(nameof(NewRequests));
        }

        [HttpPost]
        public async Task<IActionResult> RejectUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(NewRequests));
        }
    }
}
