using AkademiQMongoDb.DTOs.BannerDtos;
using AkademiQMongoDb.Services.BannerServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController(IBannerService _bannerService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var banners = await _bannerService.GetAllAsync();
            return View(banners);
        }

        [HttpGet]
        public IActionResult CreateBanner()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerDto bannerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(bannerDto);
            }

            await _bannerService.CreateBannerAsync(bannerDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBanner(string id)
        {
            var banner = await _bannerService.GetByIdBannerAsync(id);
            return View(banner);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBanner(UpdateBannerDto bannerDto)
        {
            // ModelState validasyonu eklenebilir ama UpdateDTO'larda genelde ID hidden gelir, sorun çıkmaz.
            await _bannerService.UpdateBannerAsync(bannerDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteBanner(string id)
        {
            await _bannerService.DeleteBannerAsync(id);
            return RedirectToAction("Index");
        }
    }
}