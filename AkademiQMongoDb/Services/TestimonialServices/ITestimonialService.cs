using AkademiQMongoDb.DTOs.TestimonialDTOs;

namespace AkademiQMongoDb.Services.TestimonialServices
{
    public interface ITestimonialService
    {
        Task<List<ResultTestimonialDto>> GetAllTestimonialAsync();
        Task CreateTestimonialAsync(CreateTestimonialDto createTestimonialDto);
        Task UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonialDto);
        Task DeleteTestimonialAsync(string id);
        Task<ResultTestimonialDto> GetByIdTestimonialAsync(string id);
    }
}