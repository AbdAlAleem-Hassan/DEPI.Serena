using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.PatientDoctorReviews;
using Serena.BLL.Services.PatientDoctorReviews;

namespace Serena.Presentation.Controllers
{
    public class PatientDoctorReviewController : Controller
    {
        private readonly IPatientDoctorReviewService _reviewService;

        public PatientDoctorReviewController(IPatientDoctorReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetAllAsync();
            return View(reviews);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientDoctorReviewCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _reviewService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int patientId, int doctorId)
        {
            // هنا ممكن تجيب الـ review اذا محتاج تعرضه في Edit
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientDoctorReviewCreateUpdateDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _reviewService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int patientId, int doctorId)
        {
            await _reviewService.DeleteAsync(patientId, doctorId);
            return RedirectToAction(nameof(Index));
        }
    }
}
