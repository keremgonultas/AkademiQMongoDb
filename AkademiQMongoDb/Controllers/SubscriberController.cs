using AkademiQMongoDb.DTOs.SubscriberDtos;
using AkademiQMongoDb.Services.SubscriberServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    public class SubscriberController : Controller
    {
        private readonly ISubscriberService _subscriberService;

        public SubscriberController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        [HttpPost]
        public async Task<IActionResult> SubscribeNewsletter(CreateSubscriberDto createSubscriberDto)
        {
            
            if (string.IsNullOrEmpty(createSubscriberDto.Email))
            {
                return Json(new { success = false, message = "Lütfen geçerli bir e-posta adresi giriniz." });
            }

            
            await _subscriberService.CreateAsync(createSubscriberDto);

            
            return Json(new { success = true, message = "Bültenimize başarıyla abone oldunuz! İndirimleri kaçırmayın." });
        }
    }
}