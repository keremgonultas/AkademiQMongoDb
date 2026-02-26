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

        // Ana Blog Listesi
        public async Task<IActionResult> Index()
        {
            var values = await _blogService.GetAllAsync();
            return View(values);
        }

        // Blog Detay Sayfası
        public async Task<IActionResult> BlogDetail(string id)
        {
            var value = await _blogService.GetByIdAsync(id);
            return View(value); // Sadece o blogun verisini View'a gönderiyoruz
        }
    }
}