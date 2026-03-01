using AkademiQMongoDb.Services.ContactServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        
        public async Task<IActionResult> Index()
        {
            var values = await _contactService.GetAllAsync();
            return View(values);
        }

        
        [HttpGet]
        public async Task<IActionResult> MessageDetails(string id)
        {
            var value = await _contactService.GetByIdAsync(id);
            return View(value);
        }

        
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.DeleteAsync(id);
            
            return RedirectToAction("Index", "Contact", new { area = "Admin" });
        }
    }
}