using AkademiQMongoDb.DTOs.AboutDTOs;
using AkademiQMongoDb.Services.AboutServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        
        public async Task<IActionResult> Index()
        {
            var values = await _aboutService.GetAllAboutAsync();
            return View(values);
        }

       
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await _aboutService.CreateAboutAsync(createAboutDto);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutService.DeleteAboutAsync(id);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }

        
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            var value = await _aboutService.GetByIdAboutAsync(id);

            
            var updateAboutDto = new UpdateAboutDto
            {
                Id = value.Id,
                Title = value.Title,
                SubTitle = value.SubTitle,
                Description = value.Description,
                ImageUrl = value.ImageUrl,
                VideoUrl = value.VideoUrl
            };

            return View(updateAboutDto);
        }

        
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await _aboutService.UpdateAboutAsync(updateAboutDto);
            return RedirectToAction("Index", "About", new { area = "Admin" });
        }
    }
}