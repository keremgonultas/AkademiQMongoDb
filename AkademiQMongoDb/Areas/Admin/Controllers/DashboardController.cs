using AkademiQMongoDb.Services.BlogServices;
using AkademiQMongoDb.Services.ContactServices;
using AkademiQMongoDb.Services.ProductServices; 
using AkademiQMongoDb.Services.ReservationServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IReservationService _reservationService;
        private readonly IProductService _productService;
        private readonly IBlogService _blogService;


        public DashboardController(IContactService contactService, IReservationService reservationService, IProductService productService, IBlogService blogService)
        {
            _contactService = contactService;
            _reservationService = reservationService;
            _productService = productService;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            
            var messages = await _contactService.GetAllAsync();
            ViewBag.TotalMessages = messages.Count;
            ViewBag.UnreadMessages = messages.Count(x => !x.IsRead);
            ViewBag.LastMessage = messages.LastOrDefault(); 

          
            var reservations = await _reservationService.GetAllAsync();
            ViewBag.TotalReservations = reservations.Count;
            ViewBag.PendingReservations = reservations.Count(x => x.Status == "Onay Bekliyor");
            ViewBag.LastReservation = reservations.LastOrDefault(); 

            
            var products = await _productService.GetAllAsync();
            ViewBag.TotalProducts = products.Count;
            ViewBag.LastProduct = products.LastOrDefault();


            var blogs = await _blogService.GetAllAsync();
            ViewBag.TotalBlogs = blogs.Count;
            ViewBag.LastBlog = blogs.LastOrDefault();

            return View();
        }
    }
}