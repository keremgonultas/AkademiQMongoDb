using Microsoft.AspNetCore.Mvc;
using AkademiQMongoDb.Helpers;
using AkademiQMongoDb.DTOs;

namespace AkademiQMongoDb.ViewComponents.UILayoutComponents
{
    public class _UILayoutNavbarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("FooduCart") ?? new List<CartItem>();

            ViewBag.CartCount = cart.Sum(x => x.Quantity);

           
            return View(cart);
        }
    }
}