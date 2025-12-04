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

        public async Task<IActionResult> Index()
        {
            var hospitals = await _hospitalService.GetAllHospitalsAsync();
            return View(hospitals);
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
            return View(hospital);
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
    }
}
