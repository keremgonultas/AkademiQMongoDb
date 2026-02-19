using AkademiQMongoDb.DTOs.AdminDtos;
using AkademiQMongoDb.Services.AdminServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    public class LoginController(IAdminService _adminService) : Controller
    {
        [HttpGet]
        public IActionResult Signin() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signin(LoginAdminDto loginAdminDto) 
        {
            var result = await _adminService.LoginAdminAsync(loginAdminDto);

            if (result is false)
            {
                ModelState.AddModelError("", "Kullanıcı Adı veya Şifre hatalı!");
                return View(loginAdminDto);
            }

            return Redirect("/Admin/Product/Index");
        }
    }
}