using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.Hospitals;
using Serena.BLL.Services.Hospitals;

namespace Serena.Presentation.Controllers
{
    public class HospitalController : Controller
    {
        private readonly IHospitalService _hospitalService;

        public HospitalController(IHospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        public async Task<IActionResult> Index(
            string name = "",
            string city = "",
            string department = "",
            string country = "",
            string rank = "",
            string sortBy = "name",
            decimal? maxAverageCost = null)
        {
            // Create query params for filtering
            var queryParams = new QueryParamsForHospital
            {
                Name = name,
                City = city,
                Department = department,
                Country = country,
                Rank = rank,
                MaxAverageCost = maxAverageCost,
                SortBy = sortBy
            };

            // Get filtered hospitals
            var filteredHospitals = await _hospitalService.FilterHospital(queryParams);

            // Get all unique values for filter dropdowns
            var allHospitals = await _hospitalService.GetAllHospitalsAsync();

            ViewBag.Cities = allHospitals
               .SelectMany(h => h.Address)
               .Select(a => a.City)
               .Where(c => !string.IsNullOrEmpty(c))
               .Distinct()
               .ToList();
            ViewBag.Departments = allHospitals
                .SelectMany(h => h.Departments)
                .Select(d => d.Name)
                .Distinct()
                .ToList();
            ViewBag.Cities = allHospitals
                .SelectMany(h => h.Address)
                .Select(a => a.Country)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .ToList();
            ViewBag.Ranks = allHospitals.Select(h => h.Rank).Distinct().Where(r => !string.IsNullOrEmpty(r)).ToList();

            // Set ViewBag for current filter values
            ViewBag.Name = name;
            ViewBag.City = city;
            ViewBag.Department = department;
            ViewBag.Country = country;
            ViewBag.Rank = rank;
            ViewBag.MaxAverageCost = maxAverageCost;
            ViewBag.SortBy = sortBy;
            ViewBag.TotalCount = filteredHospitals.Count;

            // Pass the filtered hospitals to the view
            return View(filteredHospitals);
        }

        [HttpGet]
        public async Task<IActionResult> FilteredHospitals(
            string name = "",
            string city = "",
            string department = "",
            string country = "",
            string rank = "",
            string sortBy = "name",
            decimal? maxAverageCost = null)
        {
            // Create query params
            var queryParams = new QueryParamsForHospital
            {
                Name = name,
                City = city,
                Department = department,
                Country = country,
                Rank = rank,
                MaxAverageCost = maxAverageCost,
                SortBy = sortBy
            };

            // Get filtered hospitals
            var filteredHospitals = await _hospitalService.FilterHospital(queryParams);

            // Return JSON response for AJAX requests
            return Json(new
            {
                hospitals = filteredHospitals.Select(h => new {
                    id = h.Id, 
                    name = h.Name,
                    rank = h.Rank,
                    averageCost = h.AverageCost,
                    hospitalPhone = h.HospitalPhone,
                    emergencyPhone = h.EmergencyPhone,
                    addresses = h.Addresses,
                    departments = h.Departments,
                    patientHospitalReviews = h.PatientHospitalReviews,
                    doctorHospitalReviews = h.DoctorHospitalReviews,
                    // إضافة خصائص إضافية للعرض
                    city = h.Addresses.FirstOrDefault()?.City,
                    country = h.Addresses.FirstOrDefault()?.Country
                }),
                totalCount = filteredHospitals.Count,
                filtersApplied = !string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(city) ||
                               !string.IsNullOrEmpty(department) || !string.IsNullOrEmpty(country) ||
                               !string.IsNullOrEmpty(rank) || maxAverageCost.HasValue
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);
            if (hospital == null) return NotFound();
            return View(hospital);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndUpdateHospitalDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _hospitalService.CreateHospitalAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);
            if (hospital == null) return NotFound();

            // تحويل إلى نموذج التعديل
            var editDto = new CreateAndUpdateHospitalDTO
            {
                Name = hospital.Name,
                Rank = hospital.Rank,
                AverageCost = hospital.AverageCost,
                HospitalPhone = hospital.HospitalPhone,
                EmergencyPhone = hospital.EmergencyPhone,
                // يمكنك إضافة المزيد من الخصائص هنا
            };

            return View(editDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateAndUpdateHospitalDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _hospitalService.UpdateHospitalAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await _hospitalService.GetHospitalByIdAsync(id);
            if (hospital == null) return NotFound();
            return View(hospital);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _hospitalService.DeleteHospitalAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [ActionName("Details")]
        public async Task<IActionResult> HospitalDetails(int id)
        {
            var hospital= await _hospitalService.GetHospitalDetails(id);
            return View(hospital);
        }
    }
}