using AkademiQMongoDb.DTOs;
using AkademiQMongoDb.Helpers;
using AkademiQMongoDb.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        
        public async Task<IActionResult> AddCartItem(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return RedirectToAction("Index", "Product");

            var cart = HttpContext.Session.GetJson<List<CartItem>>("FooduCart") ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(x => x.ProductId == id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetJson("FooduCart", cart);

            return RedirectToAction("Index", "Product");
        }

        
        public IActionResult Index()
        {
            
            var cart = HttpContext.Session.GetJson<List<CartItem>>("FooduCart") ?? new List<CartItem>();
            return View(cart);
        }

        
        public IActionResult RemoveCartItem(string id)
        {
            
            var cart = HttpContext.Session.GetJson<List<CartItem>>("FooduCart");

            if (cart != null)
            {
                
                var item = cart.FirstOrDefault(x => x.ProductId == id);
                if (item != null)
                {
                    cart.Remove(item); // Ürünü poşetten çıkar
                    HttpContext.Session.SetJson("FooduCart", cart); 
                }
            }
            
            return RedirectToAction("Index");
        }

        
        public IActionResult IncreaseCartItem(string id)
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("FooduCart");
            if (cart != null)
            {
                var item = cart.FirstOrDefault(x => x.ProductId == id);
                if (item != null)
                {
                    item.Quantity++;
                    HttpContext.Session.SetJson("FooduCart", cart);
                }
            }
            return RedirectToAction("Index");
        }

        
        public IActionResult DecreaseCartItem(string id)
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("FooduCart");
            if (cart != null)
            {
                var item = cart.FirstOrDefault(x => x.ProductId == id);
                if (item != null)
                {
                    if (item.Quantity > 1)
                    {
                        item.Quantity--; // Adet 1'den büyükse sadece azalt
                    }
                    else
                    {
                        cart.Remove(item); // Adet 1 ise ve eksiye basıldıysa, ürünü sepetten tamamen sil
                    }
                    HttpContext.Session.SetJson("FooduCart", cart);
                }
            }
            return RedirectToAction("Index");
        }
    }
}