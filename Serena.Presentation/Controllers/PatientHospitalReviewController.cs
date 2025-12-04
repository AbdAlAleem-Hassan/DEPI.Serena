using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.PatientHospitalReviews;
using Serena.BLL.Services.PatientHospitalReviews;

namespace Serena.Presentation.Controllers
{
    public class PatientHospitalReviewController : Controller
    {
        private readonly IPatientHospitalReviewService _service;

        public PatientHospitalReviewController(IPatientHospitalReviewService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int patientId, int hospitalId)
        {
            var review = await _service.GetAsync(patientId, hospitalId);
            if (review == null) return NotFound();

            return View(review);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdatePatientHospitalReviewDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int patientId, int hospitalId)
        {
            var review = await _service.GetAsync(patientId, hospitalId);
            if (review == null) return NotFound();

            var dto = new CreateAndUpdatePatientHospitalReviewDTO
            {
                Rating = review.Rating,
                Comment = review.Comment,
                PatientId = review.PatientId,
                HospitalId = review.HospitalId
            };

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int patientId, int hospitalId, CreateAndUpdatePatientHospitalReviewDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.UpdateAsync(patientId, hospitalId, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int patientId, int hospitalId)
        {
            var review = await _service.GetAsync(patientId, hospitalId);
            if (review == null) return NotFound();

            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int patientId, int hospitalId)
        {
            await _service.DeleteAsync(patientId, hospitalId);
            return RedirectToAction(nameof(Index));
        }
    }
}

