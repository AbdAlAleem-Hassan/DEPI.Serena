using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.Appointements;
using Serena.BLL.Services.Appointments;
using Serena.BLL.Services.Patients;
using Serena.DAL.Entities;

namespace Serena.Presentation.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientservice;
        private readonly UserManager<ApplicationUser> _userManager;
        public AppointmentController(IAppointmentService appointmentService, IPatientService patientservice, UserManager<ApplicationUser> userManager)
        {
            _appointmentService = appointmentService;
            _patientservice = patientservice;
            _userManager = userManager;
        }

        // GET: Appointment
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookAppointment(int doctorId, int scheduleId)
        {
            var patient = await _patientservice.GetPatientByUserIdAsync(_userManager.GetUserId(User));
            try
            {
                await _appointmentService.CreateAppointmentAsync(new CreateAndUpdateAppointmentDTO
                {
                    DoctorId = doctorId,
                    PatientId = patient.Id,
                    ScheduleId = scheduleId
                });

                return RedirectToAction("Appointments", "Patient");
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            AppointmentDTO? appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            var dto = new CreateAndUpdateAppointmentDTO
            {
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                ScheduleId = appointment.ScheduleId
            };

            return View(dto);
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateAndUpdateAppointmentDTO dto)
        {
            if (ModelState.IsValid)
            {
                await _appointmentService.UpdateAppointmentAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);
            return Ok(new {success=true,message="Appointement Canceled Successfully"});
        }
    }
}
