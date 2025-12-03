using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Models.Departments;
using Serena.BLL.Services.Departments;

namespace Serena.Presentation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var dept = await _departmentService.GetDepartmentByIdAsync(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAndUpdateDepartmentDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _departmentService.CreateDepartmentAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dept = await _departmentService.GetDepartmentByIdAsync(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateAndUpdateDepartmentDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);
            await _departmentService.UpdateDepartmentAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dept = await _departmentService.GetDepartmentByIdAsync(id);
            if (dept == null) return NotFound();
            return View(dept);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
