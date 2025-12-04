using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.DoctorHospitalReviews;
using Serena.BLL.Services.DoctorHospitalReviews;

namespace Serena.Presentation.Controllers
{
    public class DoctorHospitalReviewController : Controller
    {
        private readonly IDoctorHospitalReviewService _service;

        public DoctorHospitalReviewController(IDoctorHospitalReviewService service)
        {
            _service = service;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorHospitalReviewCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

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

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int doctorId, int hospitalId, DoctorHospitalReviewCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

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

