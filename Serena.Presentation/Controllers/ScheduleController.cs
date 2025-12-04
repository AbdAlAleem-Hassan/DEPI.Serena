using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.Schedules;
using Serena.BLL.Services.Schedules;

namespace Serena.Presentation.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _service;

        public ScheduleController(IScheduleService service)
        {
            _service = service;
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAndUpdateScheduleDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

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

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateAndUpdateScheduleDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

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
