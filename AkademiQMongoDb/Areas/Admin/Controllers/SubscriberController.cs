using AkademiQMongoDb.Services.SubscriberServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscriberController : Controller
    {
        private readonly ISubscriberService _subscriberService;

        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _subscriberService.GetAllAsync();
            return View(values);
        }

        public async Task<IActionResult> DeleteSubscriber(string id)
        {
            await _subscriberService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        
        public IActionResult SendDiscountCode(string email)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

                
                client.Credentials = new NetworkCredential("ykna2005@gmail.com", "hnvyfjlufybtkvko");
                client.EnableSsl = true;

                MailMessage message = new MailMessage();
                message.From = new MailAddress("seninmailin@gmail.com", "FOODU");
                message.To.Add(email);
                message.Subject = "Size Özel Sürpriz İndirim! 🍔";
                message.IsBodyHtml = true;

                
                message.Body = $@"
                    <div style='font-family: Arial, sans-serif; text-align: center; padding: 20px;'>
                        <h2 style='color: #dc2626;'>Merhaba!</h2>
                        <p>Bültenimize abone olduğunuz için teşekkür ederiz.</p>
                        <p>Bir sonraki siparişinizde kullanabileceğiniz <b>%20 İndirim Kodunuz:</b></p>
                        <div style='background-color: #f8f9fa; padding: 15px; font-size: 24px; font-weight: bold; border: 2px dashed #dc2626; display: inline-block; margin: 10px 0;'>
                            FOODU20
                        </div>
                        <p>Afiyet olsun!</p>
                    </div>";

                client.Send(message);
                TempData["MailSuccess"] = "İndirim kodu başarıyla gönderildi!";
            }
            catch (Exception ex)
            {
                TempData["MailError"] = "Mail gönderilirken bir hata oluştu: " + ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}