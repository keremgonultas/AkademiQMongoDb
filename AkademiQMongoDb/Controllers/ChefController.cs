using AkademiQMongoDb.Services.ChefServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    [AllowAnonymous] 
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
    }
}