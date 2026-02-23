using AkademiQMongoDb.DTOs.ChefDTOs;
using AkademiQMongoDb.Services.ChefServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")] 
    public class ChefController : Controller
    {
        private readonly IChefService _chefService;

        public ChefController(IChefService chefService)
        {
            _chefService = chefService;
        }

        
        public async Task<IActionResult> Index()
        {
            var values = await _chefService.GetAllChefAsync();
            return View(values);
        }

        
        [HttpGet]
        public IActionResult CreateChef()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateChef(CreateChefDto createChefDto)
        {
            
            createChefDto.Status = true;
            await _chefService.CreateChefAsync(createChefDto);
            return RedirectToAction("Index", "Chef", new { area = "Admin" });
        }

        
        public async Task<IActionResult> DeleteChef(string id)
        {
            await _chefService.DeleteChefAsync(id);
            return RedirectToAction("Index", "Chef", new { area = "Admin" });
        }


        [HttpGet]
        public async Task<IActionResult> UpdateChef(string id)
        {
            var value = await _chefService.GetByIdChefAsync(id);

            
            var updateChefDto = new UpdateChefDto
            {
                Id = value.Id,
                Name = value.Name,
                Title = value.Title,
                ImageUrl = value.ImageUrl,
                Status = value.Status
            };

            return View(updateChefDto); 
        }


        [HttpPost]
        public async Task<IActionResult> UpdateChef(UpdateChefDto updateChefDto)
        {
            await _chefService.UpdateChefAsync(updateChefDto);
            return RedirectToAction("Index", "Chef", new { area = "Admin" });
        }
    }
}