using AkademiQMongoDb.DTOs.ContactDtos;
using AkademiQMongoDb.Services.ContactServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Index(CreateContactDto createContactDto)
        {
            await _contactService.CreateAsync(createContactDto);

            TempData["SuccessMessage"] = "Mesajınız bize ulaştı! En kısa sürede size dönüş yapacağız.";

            return RedirectToAction("Index");
        }
    }
}