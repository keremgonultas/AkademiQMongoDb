using AkademiQMongoDb.Services.BlogServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        
        public async Task<IActionResult> Index()
        {
            var values = await _blogService.GetAllAsync();
            return View(values);
        }

        
        public async Task<IActionResult> BlogDetail(string id)
        {
           
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            var value = await _blogService.GetByIdAsync(id);

            
            if (value == null)
            {
                return RedirectToAction("Index");
            }

            return View(value);
        }
    }
}