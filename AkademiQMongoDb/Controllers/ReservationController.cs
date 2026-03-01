using AkademiQMongoDb.DTOs.ReservationDtos;
using AkademiQMongoDb.Services.ReservationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    [AllowAnonymous]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateReservationDto createReservationDto)
        {
            await _reservationService.CreateAsync(createReservationDto);

            // SweetAlert için başarı mesajımızı hazırlıyoruz
            TempData["SuccessMessage"] = "Rezervasyon talebiniz başarıyla alındı! Müsaitlik durumuna göre en kısa sürede onaylanacaktır.";

            return RedirectToAction("Index");
        }
    }
}