using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Serena.DAL.Entities;

namespace Serena.BLL.Services.Contact
{
    public class SendEmailService
    {
        public async Task SendEmailAsync(ContactUs dto)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("mo4026838@gmail.com", "My Website"),
                Subject = dto.Subject,
                Body = $"From: {dto.Name}\nEmail: {dto.Email}\n\nMessage:\n{dto.Message}",
                IsBodyHtml = false
            };

            mailMessage.To.Add("admin@example.com"); // البريد اللي هيستقبل الرسائل

            using var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("mo4026838@gmail.com", "ctpm gxvg ynme txbj"), // استخدم الباسورد من App Password
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
