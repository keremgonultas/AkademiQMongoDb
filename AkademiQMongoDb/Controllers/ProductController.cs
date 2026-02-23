using AkademiQMongoDb.DTOs.ProductDtos;
using AkademiQMongoDb.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AkademiQMongoDb.Controllers
{
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private readonly IProductService _productService; 

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            
            var values = await _productService.GetAllAsync();
            return View(values);
        }
    }
}