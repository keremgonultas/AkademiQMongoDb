using AkademiQMongoDb.Services.TestimonialServices;
using Microsoft.AspNetCore.Mvc;

namespace AkademiQMongoDb.ViewComponents.TestimonialComponents
{
    public class _TestimonialViewComponent : ViewComponent
    {
        private readonly ITestimonialService _testimonialService;

        public _TestimonialViewComponent(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _testimonialService.GetAllTestimonialAsync();

            
            var activeTestimonials = values.Where(x => x.Status == true).ToList();

            return View(activeTestimonials);
        }
    }
}