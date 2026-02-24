using AkademiQMongoDb.DTOs.TestimonialDTOs;
using AkademiQMongoDb.Services.TestimonialServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _testimonialService.GetAllTestimonialAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            await _testimonialService.CreateTestimonialAsync(createTestimonialDto);
            return RedirectToAction("Index", "Testimonial", new { area = "Admin" });
        }

        public async Task<IActionResult> DeleteTestimonial(string id)
        {
            await _testimonialService.DeleteTestimonialAsync(id);
            return RedirectToAction("Index", "Testimonial", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(string id)
        {
            var value = await _testimonialService.GetByIdTestimonialAsync(id);
            var updateTestimonialDto = new UpdateTestimonialDto
            {
                Id = value.Id,
                Title = value.Title,
                Comment = value.Comment,
                ImageUrl = value.ImageUrl,
                FoodImageUrl = value.FoodImageUrl,
                Status = value.Status
            };
            return View(updateTestimonialDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            await _testimonialService.UpdateTestimonialAsync(updateTestimonialDto);
            return RedirectToAction("Index", "Testimonial", new { area = "Admin" });
        }
    }
}