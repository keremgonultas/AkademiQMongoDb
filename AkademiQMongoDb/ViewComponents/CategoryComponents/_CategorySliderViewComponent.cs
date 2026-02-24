using AkademiQMongoDb.Services.CategoryServices; // Kendi yoluna göre düzelt
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents.CategoryComponents
{
    public class _CategorySliderViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _CategorySliderViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Sitede gösterilecek aktif kategorileri çekiyoruz
            var values = await _categoryService.GetAllAsync();
            return View(values);
        }
    }
}