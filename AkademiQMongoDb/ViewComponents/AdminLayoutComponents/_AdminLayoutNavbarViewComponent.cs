using AkademiQMongoDb.Services.AdminServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents.AdminLayoutComponents
{
    public class _AdminLayoutNavbarViewComponent(IAdminService adminService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // 1. Session'dan kullanıcı adını okumaya çalış
            var userName = HttpContext.Session.GetString("Username");

            // 2. Eğer Session silinmişse (ki direkt projeyi çalıştırınca silinmiş oluyor) programı çökertme!
            if (string.IsNullOrEmpty(userName))
            {
                ViewBag.fullName = "Giriş Yapılmadı";
                return View();
            }

            // 3. Veritabanından admini bul
            var admin = await adminService.GetAdminByUsernameAsync(userName);

            // 4. Veritabanında bir hata olduysa veya kullanıcı silindiyse yine programı çökertme!
            if (admin == null)
            {
                ViewBag.fullName = "Kullanıcı Bulunamadı";
                return View();
            }

            // 5. Her şey yolundaysa ismini yazdır!
            ViewBag.fullName = string.Join(" ", admin.FirstName, admin.LastName);
            return View();
        }
    }
}