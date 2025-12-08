using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.Schedules;
using Serena.BLL.Services.Schedules;

namespace Serena.Presentation.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _service;
        private readonly Serena.BLL.Services.Doctors.IDoctorService _doctorService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<Serena.DAL.Entities.ApplicationUser> _userManager;

        public ScheduleController(IScheduleService service, Serena.BLL.Services.Doctors.IDoctorService doctorService, Microsoft.AspNetCore.Identity.UserManager<Serena.DAL.Entities.ApplicationUser> userManager)
        {
            _service = service;
            _doctorService = doctorService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var schedules = await _service.GetAllSchedulesAsync();
            return View(schedules);
        }

        public async Task<IActionResult> Details(int id)
        {
            var schedule = await _service.GetScheduleByIdAsync(id);
            if (schedule == null) return NotFound();

            return View(schedule);
        }

        public async Task<IActionResult> Create()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            ViewBag.DoctorsList = doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName });
            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ViewBag.DoctorsList, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdateScheduleDTO dto)
        {
            if (!ModelState.IsValid) 
            {
               var doctors = await _doctorService.GetAllDoctorsAsync();
               ViewBag.DoctorsList = doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName });
               ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ViewBag.DoctorsList, "Id", "Name", dto.DoctorId);
               return View(dto);
            }


            var selectedDoctor = await _doctorService.GetDoctorByIdAsync(dto.DoctorId);
            if (selectedDoctor != null)
            {
                dto.DoctorUserId = selectedDoctor.UserId;
            }

            await _service.CreateScheduleAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var schedule = await _service.GetScheduleByIdAsync(id);
            if (schedule == null) return NotFound();

            var dto = new CreateAndUpdateScheduleDTO
            {
                DoctorId = schedule.DoctorId,
                Date = schedule.Date,
                Price = schedule.Price
            };

            var doctors = await _doctorService.GetAllDoctorsAsync();
            ViewBag.DoctorsList = doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName });
            ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ViewBag.DoctorsList, "Id", "Name", dto.DoctorId);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateAndUpdateScheduleDTO dto)
        {
            if (!ModelState.IsValid)
            {
               var doctors = await _doctorService.GetAllDoctorsAsync();
               ViewBag.DoctorsList = doctors.Select(d => new { Id = d.Id, Name = d.FirstName + " " + d.LastName });
               ViewBag.Doctors = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(ViewBag.DoctorsList, "Id", "Name", dto.DoctorId);
               return View(dto);
            }

            await _service.UpdateScheduleAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await _service.GetScheduleByIdAsync(id);
            if (schedule == null) return NotFound();

            return View(schedule);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await _service.DeleteScheduleAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
