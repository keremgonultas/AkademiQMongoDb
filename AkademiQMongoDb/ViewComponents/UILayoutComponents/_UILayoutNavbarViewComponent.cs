using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents.UILayoutComponents
{
    public class _UILayoutNavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}