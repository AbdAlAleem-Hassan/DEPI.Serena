using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serena.BLL.Models.Home;
using Serena.BLL.Services.Doctors;
using Serena.BLL.Services.Hospitals;
using Serena.BLL.Services.Patients;
using Serena.DAL.Entities;
using Serena.Presentation.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Serena.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHospitalService _hospitalService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IHospitalService hospitalService,
            IDoctorService doctorService,
            IPatientService patientService,
            ILogger<HomeController> logger)
        {
            _hospitalService = hospitalService;
            _doctorService = doctorService;
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                // Get statistics data
                var hospitals = await _hospitalService.GetAllHospitalsAsync();
                var doctors = await _doctorService.GetAllDoctorsAsync();
                var patients = await _patientService.GetAllPatientsAsync();

                // Calculate real statistics
                var totalHospitals = hospitals.Count();
                var totalDoctors = doctors.Count();
                var totalPatients = patients.Count();

                // Get top hospitals (by rating or average cost)
                var topHospitals = hospitals
                    .OrderByDescending(h => h.AverageCost)
                    .Take(6)
                    .ToList();

                // Get top doctors (by experience)
                var topDoctors = doctors
                    .OrderByDescending(d => d.YearsOfExperience)
                    .Take(6)
                    .ToList();

                // Create view model
                var model = new HomeIndexViewModel
                {
                    TotalHospitals = totalHospitals,
                    TotalDoctors = totalDoctors,
                    TotalPatients = totalPatients,
                    TopHospitals = topHospitals,
                    TopDoctors = topDoctors,
                    TodayAppointments = 245, // This could come from AppointmentService
                    HealthScore = 98 // This could come from analytics service
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page");

                // Return default model in case of error
                var model = new HomeIndexViewModel
                {
                    TotalHospitals = 0,
                    TotalDoctors = 0,
                    TotalPatients = 0,
                    TopHospitals = new List<Serena.BLL.Models.Hospitals.HospitalDTO>(),
                    TopDoctors = new List<Serena.BLL.Models.Doctors.DoctorDTO>(),
                    TodayAppointments = 245,
                    HealthScore = 98
                };

                TempData["Warning"] = "⚠️ Unable to load some data. Please try again later.";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Here you would save the contact message to database
                // For now, we'll just show a success message

                TempData["Success"] = "📧 Thank you for your message! We'll get back to you soon.";
                return RedirectToAction(nameof(Contact));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving contact message");
                TempData["Error"] = "❌ Failed to send message. Please try again.";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Terms()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FAQ()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Services()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query, string type = "all")
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var searchResults = new SearchResultsViewModel
                {
                    Query = query,
                    Type = type
                };

                if (type == "all" || type == "hospitals")
                {
                    var hospitals = await _hospitalService.GetAllHospitalsAsync();
                    searchResults.Hospitals = hospitals
                        .Where(h => h.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                   h.Rank.Contains(query, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (type == "all" || type == "doctors")
                {
                    var doctors = await _doctorService.GetAllDoctorsAsync();
                    searchResults.Doctors = doctors
                        .Where(d => d.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                   d.LastName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                   d.Specialization.Contains(query, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                return View(searchResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during search");
                TempData["Error"] = "❌ Search failed. Please try again.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [ResponseCache(Duration = 3600)] // Cache for 1 hour
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var hospitals = await _hospitalService.GetAllHospitalsAsync();
                var doctors = await _doctorService.GetAllDoctorsAsync();
                var patients = await _patientService.GetAllPatientsAsync();

                var stats = new
                {
                    totalHospitals = hospitals.Count(),
                    totalDoctors = doctors.Count(),
                    totalPatients = patients.Count(),
                    lastUpdated = DateTime.UtcNow
                };

                return Json(stats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting stats");
                return Json(new { error = "Failed to load statistics" });
            }
        }
    }
}