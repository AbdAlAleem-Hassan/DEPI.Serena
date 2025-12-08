using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.DoctorHospitalReviews;
using Serena.BLL.Services.DoctorHospitalReviews;

namespace Serena.Presentation.Controllers
{
    public class DoctorHospitalReviewController : Controller
    {
        private readonly IDoctorHospitalReviewService _service;
        private readonly Serena.BLL.Services.Doctors.IDoctorService _doctorService;
        private readonly Serena.BLL.Services.Hospitals.IHospitalService _hospitalService;

        public DoctorHospitalReviewController(IDoctorHospitalReviewService service, Serena.BLL.Services.Doctors.IDoctorService doctorService, Serena.BLL.Services.Hospitals.IHospitalService hospitalService)
        {
            _service = service;
            _doctorService = doctorService;
            _hospitalService = hospitalService;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _service.GetAllAsync();
            return View(reviews);
        }

        public async Task<IActionResult> Details(int doctorId, int hospitalId)
        {
            var review = await _service.GetAsync(doctorId, hospitalId);
            if (review == null) return NotFound();

            return View(review);
        }

        public async Task<IActionResult> Create()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName }), "Id", "Name");
            var hospitals = await _hospitalService.GetAllHospitalsAsync();
            ViewBag.Hospitals = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(hospitals, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorHospitalReviewCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) 
            {
                var doctors = await _doctorService.GetAllDoctorsAsync();
                ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName }), "Id", "Name", dto.DoctorId);
                var hospitals = await _hospitalService.GetAllHospitalsAsync();
                ViewBag.Hospitals = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(hospitals, "Id", "Name", dto.HospitalId);
                return View(dto);
            }

            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int doctorId, int hospitalId)
        {
            var review = await _service.GetAsync(doctorId, hospitalId);
            if (review == null) return NotFound();

            var dto = new DoctorHospitalReviewCreateUpdateDTO
            {
                Rating = review.Rating,
                Comment = review.Comment,
                DoctorId = review.DoctorId,
                HospitalId = review.HospitalId
            };

            var doctors = await _doctorService.GetAllDoctorsAsync();
            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName }), "Id", "Name", dto.DoctorId);
            var hospitals = await _hospitalService.GetAllHospitalsAsync();
            ViewBag.Hospitals = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(hospitals, "Id", "Name", dto.HospitalId);
            
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int doctorId, int hospitalId, DoctorHospitalReviewCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) 
            {
                var doctors = await _doctorService.GetAllDoctorsAsync();
                ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName }), "Id", "Name", dto.DoctorId);
                var hospitals = await _hospitalService.GetAllHospitalsAsync();
                ViewBag.Hospitals = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(hospitals, "Id", "Name", dto.HospitalId);
                return View(dto);
            }

            await _service.UpdateAsync(doctorId, hospitalId, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int doctorId, int hospitalId)
        {
            var review = await _service.GetAsync(doctorId, hospitalId);
            if (review == null) return NotFound();

            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int doctorId, int hospitalId)
        {
            await _service.DeleteAsync(doctorId, hospitalId);
            return RedirectToAction(nameof(Index));
        }
    }
}

