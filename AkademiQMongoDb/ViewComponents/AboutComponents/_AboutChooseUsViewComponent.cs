using AkademiQMongoDb.Services.AboutServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents.AboutComponents
{
    public class _AboutChooseUsViewComponent : ViewComponent
    {
        private readonly IAboutService _aboutService;

        public _AboutChooseUsViewComponent(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Videomuzun linki About tablosunda olduğu için yine aynı servisi çağırıyoruz.
            var values = await _aboutService.GetAllAboutAsync();
            var value = values.FirstOrDefault();
            return View(value);
        }
    }
}