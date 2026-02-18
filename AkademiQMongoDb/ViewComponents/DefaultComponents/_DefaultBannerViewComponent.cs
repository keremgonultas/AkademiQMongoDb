using AkademiQMongoDb.Services.BannerServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents.DefaultComponents
{
    public class _DefaultBannerViewComponent : ViewComponent
    {
        private readonly IBannerService _bannerService;

        public _DefaultBannerViewComponent(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var banner = await _bannerService.GetAllAsync();
            return View(banner);
        }
    }
}
