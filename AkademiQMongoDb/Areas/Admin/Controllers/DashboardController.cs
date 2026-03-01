using AkademiQMongoDb.Services.ContactServices;
using AkademiQMongoDb.Services.ReservationServices;
using AkademiQMongoDb.Services.ProductServices; // Ürün servisimizi ekledik
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IReservationService _reservationService;
        private readonly IProductService _productService; 

        
        public DashboardController(IContactService contactService, IReservationService reservationService, IProductService productService)
        {
            _contactService = contactService;
            _reservationService = reservationService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            
            var messages = await _contactService.GetAllAsync();
            ViewBag.TotalMessages = messages.Count;
            ViewBag.UnreadMessages = messages.Count(x => !x.IsRead);

            var reservations = await _reservationService.GetAllAsync();
            ViewBag.TotalReservations = reservations.Count;
            ViewBag.PendingReservations = reservations.Count(x => x.Status == "Onay Bekliyor");

            
            var products = await _productService.GetAllAsync();
            ViewBag.TotalProducts = products.Count;

            
            ViewBag.TotalBlogs = 12;

            return View();
        }
    }
}