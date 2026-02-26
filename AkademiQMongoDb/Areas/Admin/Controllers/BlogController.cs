using AkademiQMongoDb.DTOs.BlogDtos;
using AkademiQMongoDb.Services.BlogServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")] 
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

        
        [HttpGet]
        public IActionResult CreateBlog()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto)
        {
            await _blogService.CreateAsync(createBlogDto);
            return RedirectToAction("Index", "Blog", new { area = "Admin" });
        }

        
        public async Task<IActionResult> DeleteBlog(string id)
        {
            await _blogService.DeleteAsync(id);
            return RedirectToAction("Index", "Blog", new { area = "Admin" });
        }

        
        [HttpGet]
        public async Task<IActionResult> UpdateBlog(string id)
        {
            var value = await _blogService.GetByIdAsync(id);
            return View(value); 
        }

        
        [HttpPost]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
        {
            await _blogService.UpdateAsync(updateBlogDto);
            return RedirectToAction("Index", "Blog", new { area = "Admin" });
        }
    }
}