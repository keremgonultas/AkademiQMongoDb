using AkademiQMongoDb.DTOs.GalleryDtos;
using AkademiQMongoDb.Services.GalleryServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryController : Controller
    {
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _galleryService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateGallery()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGallery(CreateGalleryDto createGalleryDto)
        {
            await _galleryService.CreateAsync(createGalleryDto);
            return RedirectToAction("Index", "Gallery", new { area = "Admin" });
        }

        public async Task<IActionResult> DeleteGallery(string id)
        {
            await _galleryService.DeleteAsync(id);
            return RedirectToAction("Index", "Gallery", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateGallery(string id)
        {
            var value = await _galleryService.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGallery(UpdateGalleryDto updateGalleryDto)
        {
            await _galleryService.UpdateAsync(updateGalleryDto);
            return RedirectToAction("Index", "Gallery", new { area = "Admin" });
        }
    }
}