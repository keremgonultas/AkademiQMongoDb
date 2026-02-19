using AkademiQMongoDb.DTOs.AdminDtos;
using AkademiQMongoDb.Services.AdminServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Controllers
{
    public class RegisterController(IAdminService _adminService) : Controller
    {
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Signup(RegisterAdminDto registerAdminDto)
        {
            await _adminService.CreateAdminAsync(registerAdminDto);
            return RedirectToAction("Signin", "Login");
        }
    }
}
