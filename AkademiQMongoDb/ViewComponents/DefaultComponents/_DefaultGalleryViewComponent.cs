using AkademiQMongoDb.Services.GalleryServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents.DefaultViewComponents
{
    public class _DefaultGalleryViewComponent : ViewComponent
    {
        private readonly IGalleryService _galleryService;

        public _DefaultGalleryViewComponent(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _galleryService.GetAllAsync();
            return View(values);
        }
    }
}