using AkademiQMongoDb.Services.AboutServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents.AboutComponents
{
    
    public class _AboutSectionViewComponent : ViewComponent
    {
        private readonly IAboutService _aboutService;

        public _AboutSectionViewComponent(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _aboutService.GetAllAboutAsync();
            var value = values.FirstOrDefault();
            return View(value);
        }
    }
}