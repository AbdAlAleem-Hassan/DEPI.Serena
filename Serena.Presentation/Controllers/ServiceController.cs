using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.DoctorService;
using Serena.BLL.Services.DoctorServices;

namespace Serena.Presentation.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllServicesAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int id)
        {
            var service = await _service.GetServiceByIdAsync(id);
            if (service == null) return NotFound();

            return View(service);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdateServiceDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.CreateServiceAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var service = await _service.GetServiceByIdAsync(id);
            if (service == null) return NotFound();

            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateAndUpdateServiceDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _service.UpdateServiceAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var service = await _service.GetServiceByIdAsync(id);
            if (service == null) return NotFound();

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await _service.DeleteServiceAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

