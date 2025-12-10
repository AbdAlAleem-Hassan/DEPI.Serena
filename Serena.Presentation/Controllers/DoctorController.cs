using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serena.BLL.Models.Doctors;
using Serena.BLL.Models.Schedules;
using Serena.BLL.Services.Appointments;
using Serena.BLL.Services.Doctors;
using Serena.BLL.Services.Schedules;
using Serena.DAL.Entities;
using Serena.DAL.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serena.Presentation.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IUnitOfWork _unitOfWork;
       // private readonly IAppointmentService _appointmentService;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IScheduleService _service;

        public DoctorController(IDoctorService doctorService, IUnitOfWork unitOfWork, IScheduleService service, UserManager<ApplicationUser> userManager, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _unitOfWork = unitOfWork;
            _service = service;
            _userManager = userManager;
          //  _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            string name = "",
            string specialization = "",
            string city = "",
            int yearsOfExperience = 0,
            string sortBy = "name_asc",
            int pageNumber = 1,
            int pageSize = 12)
        {
            // Create query parameters
            var queryParams = new QueryParams
            {
                Name = name,
                Specialization = specialization,
                City = city,
                YearsOfExperience = yearsOfExperience,
                SortBy = sortBy,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            // Get filtered doctors using your FilterDoctors method
            var doctors = await _doctorService.FilterDoctors(queryParams);

            // Get total count for pagination (without applying pagination in the query)
            var totalQuery = _unitOfWork.DoctorRepository.GetIQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                totalQuery = totalQuery.Where(d => (d.FirstName + " " + d.LastName).Contains(name));
            }

            if (!string.IsNullOrEmpty(specialization))
            {
                totalQuery = totalQuery.Where(d => d.Specialization == specialization);
            }

            if (!string.IsNullOrEmpty(city))
            {
                totalQuery = totalQuery.Where(d => d.City == city);
            }

            if (yearsOfExperience > 0)
            {
                totalQuery = totalQuery.Where(d => d.YearsOfExperience >= yearsOfExperience);
            }

            var totalCount = await totalQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Get distinct values for filter dropdowns
            var specializations = await _unitOfWork.DoctorRepository.GetIQueryable()
                .Select(d => d.Specialization)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            var cities = await _unitOfWork.DoctorRepository.GetIQueryable()
                .Where(d => !string.IsNullOrEmpty(d.City))
                .Select(d => d.City)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            // Pass data to view
            ViewBag.Name = name;
            ViewBag.Specialization = specialization;
            ViewBag.City = city;
            ViewBag.YearsOfExperience = yearsOfExperience;
            ViewBag.SortBy = sortBy;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.TotalPages = totalPages;
            ViewBag.Specializations = specializations;
            ViewBag.Cities = cities;

            return View(doctors);
        }

        [HttpGet("Doctor/FilteredDoctors")]
        public async Task<IActionResult> FilteredDoctors(
            string name = "",
            string specialization = "",
            string city = "",
            int yearsOfExperience = 0,
            string sortBy = "name_asc",
            int pageNumber = 1,
            int pageSize = 12)
        {
            try
            {
                Console.WriteLine($"Filter request received: name={name}, specialization={specialization}, city={city}, years={yearsOfExperience}, sort={sortBy}, page={pageNumber}, pageSize={pageSize}");

                // Create query parameters
                var queryParams = new QueryParams
                {
                    Name = name,
                    Specialization = specialization,
                    City = city,
                    YearsOfExperience = yearsOfExperience,
                    SortBy = sortBy,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                // Get filtered doctors using your FilterDoctors method
                var doctors = await _doctorService.FilterDoctors(queryParams);
                Console.WriteLine($"Doctors found: {doctors?.Count ?? 0}");

                // Get total count for pagination
                var totalQuery = _unitOfWork.DoctorRepository.GetIQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    totalQuery = totalQuery.Where(d => (d.FirstName + " " + d.LastName).Contains(name));
                }

                if (!string.IsNullOrEmpty(specialization))
                {
                    totalQuery = totalQuery.Where(d => d.Specialization == specialization);
                }

                if (!string.IsNullOrEmpty(city))
                {
                    totalQuery = totalQuery.Where(d => d.City == city);
                }

                if (yearsOfExperience > 0)
                {
                    totalQuery = totalQuery.Where(d => d.YearsOfExperience >= yearsOfExperience);
                }

                var totalCount = await totalQuery.CountAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

                Console.WriteLine($"Total count: {totalCount}, Total pages: {totalPages}");

                // Ensure doctors is not null
                doctors ??= new List<DoctorDetailsDTO?>();

                var response = new
                {
                    Doctors = doctors,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    Success = true
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in FilteredDoctors: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Return error response
                return Json(new
                {
                    Success = false,
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    Doctors = new List<DoctorDetailsDTO>(),
                    TotalCount = 0,
                    TotalPages = 0,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                });
            }
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndUpdateDoctorDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _doctorService.CreateDoctorAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateAndUpdateDoctorDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _doctorService.UpdateDoctorAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Patients")]
        public IActionResult Patients()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Schedule()
        {
            var doctor = await _userManager.GetUserAsync(User);
            ViewBag.DoctorUserId = doctor.Id;

            var schedules = await _service.GetDoctorSchedulesAsync(doctor.Id);
            return View(schedules);
        }


        [HttpPost]
        public async Task<IActionResult> Schedule(CreateAndUpdateScheduleDTO model)
        {
            if (!ModelState.IsValid)
                return View(await _service.GetAllSchedulesAsync());
            var doctor = _userManager.GetUserId(User);
            model.DoctorUserId = doctor;

            var result = await _service.CreateScheduleAsync(model);

            if (result == 0)
            {
                ModelState.AddModelError("", "Doctor not found.");
                return View(await _service.GetAllSchedulesAsync());
            }

            return RedirectToAction("Schedule");
        }


        [HttpGet("SearchSuggestions")]
        public async Task<IActionResult> SearchSuggestions(string term)
        {
            try
            {
                if (string.IsNullOrEmpty(term) || term.Length < 2)
                {
                    return Json(new List<string>());
                }

                var doctors = await _doctorService.GetAllDoctorsAsync();
                var suggestions = doctors
                    .Where(d =>
                        (d.FirstName + " " + d.LastName).Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        d.Specialization.Contains(term, StringComparison.OrdinalIgnoreCase)
                    )
                    .Select(d => new
                    {
                        Name = $"{d.FirstName} {d.LastName}",
                        Specialization = d.Specialization,
                        Id = d.Id
                    })
                    .Take(10)
                    .ToList();

                return Json(suggestions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchSuggestions: {ex.Message}");
                return Json(new List<string>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> DoctorProfile(int id)
        {
            try
            {
                var doctor = await _doctorService.GetDoctorByIdAsync(id);
                if (doctor == null)
                {
                    return View(new { Success = false, Message = "Doctor not found." });
                }
                return View( doctor );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetDoctorDetails: {ex.Message}");
                return View(new { Success = false, Message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Appointments()
        {
            var doctor = await _doctorService.GetDoctorByUserIdAsync(_userManager.GetUserId(User));
            var result =await _doctorService.GetPatientForDoctor(doctor.Id);
            return View(result);
        }
    }
}