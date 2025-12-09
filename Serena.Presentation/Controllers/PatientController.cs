using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.Patients;
using Serena.BLL.Services.Appointments;
using Serena.BLL.Services.Patients;
using Serena.DAL.Entities;

namespace Serena.Presentation.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppointmentService _appointmentService;


        public PatientController(IPatientService patientService, UserManager<ApplicationUser> userManager, IAppointmentService appointmentService)
        {
            _patientService = patientService;
            _userManager = userManager;
            _appointmentService = appointmentService;
        }

        // GET: /Patient
        public async Task<IActionResult> Index()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return View(patients); // View: Views/Patient/Index.cshtml
        }

        // GET: /Patient/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        // GET: /Patient/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndUpdatePatientDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _patientService.CreatePatientAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Patient/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        // POST: /Patient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateAndUpdatePatientDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            await _patientService.UpdatePatientAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Patient/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        // POST: /Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _patientService.DeletePatientAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Appointments()
        {
            var patient = await _patientService.GetPatientByUserIdAsync(_userManager.GetUserId(User));
            var appointments = await _appointmentService.GetAppointmentsByPatientIdAsync(patient.Id);
            return View(appointments);

        }
    }
}
