using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.DoctorService;
using Serena.BLL.Services.DoctorServices;

namespace Serena.Presentation.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServiceService _service;
        private readonly Serena.BLL.Services.Doctors.IDoctorService _doctorService;

        public ServiceController(IServiceService service, Serena.BLL.Services.Doctors.IDoctorService doctorService)
        {
            _service = service;
            _doctorService = doctorService;
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

        public async Task<IActionResult> Create()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(doctors, "Id", "FirstName"); // Or Full Name if DTO has it
            // checking DoctorDTO properties. usually FirstName/LastName. 
            // Better to concat? SelectList can take a selected value. 
            // Let's assume FirstName for now or fix later if DTO has FullName.
            ViewBag.DoctorsList = doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName });
            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ViewBag.DoctorsList, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdateServiceDTO dto)
        {
            if (!ModelState.IsValid) 
            {
               var doctors = await _doctorService.GetAllDoctorsAsync();
               ViewBag.DoctorsList = doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName });
               ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ViewBag.DoctorsList, "Id", "Name", dto.DoctorId);
               return View(dto);
            }

            await _service.CreateServiceAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var service = await _service.GetServiceByIdAsync(id);
            if (service == null) return NotFound();

            var doctors = await _doctorService.GetAllDoctorsAsync();
            ViewBag.DoctorsList = doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName });
            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ViewBag.DoctorsList, "Id", "Name", service.DoctorId);

            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateAndUpdateServiceDTO dto)
        {
            if (!ModelState.IsValid)
            {
               var doctors = await _doctorService.GetAllDoctorsAsync();
               ViewBag.DoctorsList = doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName });
               ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ViewBag.DoctorsList, "Id", "Name", dto.DoctorId);
               return View(dto);
            }

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

