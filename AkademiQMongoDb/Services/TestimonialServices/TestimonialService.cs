using AkademiQMongoDb.DTOs.TestimonialDTOs;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using MongoDB.Driver;

namespace AkademiQMongoDb.Services.TestimonialServices
{
    public class TestimonialService : ITestimonialService
    {
        private readonly IMongoCollection<Testimonial> _testimonialCollection;

        public TestimonialService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            // Veritabanında "Testimonials" adında bir tablo (collection) oluşturacak
            _testimonialCollection = database.GetCollection<Testimonial>("Testimonials");
        }

        public async Task CreateTestimonialAsync(CreateTestimonialDto createTestimonialDto)
        {
            var testimonial = new Testimonial
            {
                Title = createTestimonialDto.Title,
                Comment = createTestimonialDto.Comment,
                ImageUrl = createTestimonialDto.ImageUrl,
                FoodImageUrl = createTestimonialDto.FoodImageUrl,
                Status = createTestimonialDto.Status
            };
            await _testimonialCollection.InsertOneAsync(testimonial);
        }

        public async Task DeleteTestimonialAsync(string id)
        {
            await _testimonialCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultTestimonialDto>> GetAllTestimonialAsync()
        {
            var values = await _testimonialCollection.Find(x => true).ToListAsync();
            return values.Select(x => new ResultTestimonialDto
            {
                Id = x.Id,
                Title = x.Title,
                Comment = x.Comment,
                ImageUrl = x.ImageUrl,
                FoodImageUrl = x.FoodImageUrl,
                Status = x.Status
            }).ToList();
        }

        public async Task<ResultTestimonialDto> GetByIdTestimonialAsync(string id)
        {
            var value = await _testimonialCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return new ResultTestimonialDto
            {
                Id = value.Id,
                Title = value.Title,
                Comment = value.Comment,
                ImageUrl = value.ImageUrl,
                FoodImageUrl = value.FoodImageUrl,
                Status = value.Status
            };
        }

        public async Task UpdateTestimonialAsync(UpdateTestimonialDto updateTestimonialDto)
        {
            var testimonial = new Testimonial
            {
                Id = updateTestimonialDto.Id,
                Title = updateTestimonialDto.Title,
                Comment = updateTestimonialDto.Comment,
                ImageUrl = updateTestimonialDto.ImageUrl,
                FoodImageUrl = updateTestimonialDto.FoodImageUrl,
                Status = updateTestimonialDto.Status
            };
            await _testimonialCollection.FindOneAndReplaceAsync(x => x.Id == updateTestimonialDto.Id, testimonial);
        }
    }
}