using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents.DefaultViewComponents
{
    public class _DefaultFeatureViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}