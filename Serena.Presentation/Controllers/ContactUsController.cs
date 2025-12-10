using Microsoft.AspNetCore.Mvc;
using Serena.BLL.Services.Contact;
using Serena.DAL.Entities;

namespace Serena.Presentation.Controllers
{
    public class ContactUsController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs(ContactUs model, [FromServices] SendEmailService emailService)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await emailService.SendEmailAsync(model); // استدعاء instance method

                TempData["Success"] = "✅ Your message has been sent successfully!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"❌ Error sending message: {ex.Message}";
                return View(model);
            }
        }


    }
}
