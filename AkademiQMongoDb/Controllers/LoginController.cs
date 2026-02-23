using AkademiQMongoDb.DTOs.AdminDtos;
using AkademiQMongoDb.Services.AdminServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AkademiQMongoDb.Controllers
{
    [AllowAnonymous]
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

            //Cookie Oluşturma
            var admin = await _adminService.GetAdminByUsernameAsync(loginAdminDto.Username);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, admin.Username),
                new Claim("fullname", string.Join(" ",admin.FirstName, admin.LastName))
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30),
                IsPersistent = false,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            HttpContext.Session.SetString("Username", admin.Username);

            return Redirect("/Admin/Product/Index");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Index","Default");
        }

    }
}