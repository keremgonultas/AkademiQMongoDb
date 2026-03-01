using AkademiQMongoDb.Services.ReservationServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _reservationService.GetAllAsync();
            return View(values);
        }

        // Onaylama İşlemi
        public async Task<IActionResult> ApproveReservation(string id)
        {
            await _reservationService.ApproveReservationAsync(id);
            return RedirectToAction("Index", "Reservation", new { area = "Admin" });
        }

        // İptal Etme İşlemi
        public async Task<IActionResult> CancelReservation(string id)
        {
            await _reservationService.CancelReservationAsync(id);
            return RedirectToAction("Index", "Reservation", new { area = "Admin" });
        }

        // Silme İşlemi (Tamamen Temizleme)
        public async Task<IActionResult> DeleteReservation(string id)
        {
            await _reservationService.DeleteAsync(id);
            return RedirectToAction("Index", "Reservation", new { area = "Admin" });
        }
    }
}