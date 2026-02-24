using AkademiQMongoDb.DTOs.AboutDTOs;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using MongoDB.Driver;

namespace AkademiQMongoDb.Services.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly IMongoCollection<About> _aboutCollection;

        public AboutService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            // Veritabanında "Abouts" adında bir tablo oluşturacak
            _aboutCollection = database.GetCollection<About>("Abouts");
        }

        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            var about = new About
            {
                SubTitle = createAboutDto.SubTitle,
                Title = createAboutDto.Title,
                Description = createAboutDto.Description,
                ImageUrl = createAboutDto.ImageUrl,
                VideoUrl = createAboutDto.VideoUrl
            };
            await _aboutCollection.InsertOneAsync(about);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await _aboutCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            var values = await _aboutCollection.Find(x => true).ToListAsync();
            return values.Select(x => new ResultAboutDto
            {
                Id = x.Id,
                SubTitle = x.SubTitle,
                Title = x.Title,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                VideoUrl = x.VideoUrl
            }).ToList();
        }

        public async Task<ResultAboutDto> GetByIdAboutAsync(string id)
        {
            var value = await _aboutCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return new ResultAboutDto
            {
                Id = value.Id,
                SubTitle = value.SubTitle,
                Title = value.Title,
                Description = value.Description,
                ImageUrl = value.ImageUrl,
                VideoUrl = value.VideoUrl
            };
        }

        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            var about = new About
            {
                Id = updateAboutDto.Id,
                SubTitle = updateAboutDto.SubTitle,
                Title = updateAboutDto.Title,
                Description = updateAboutDto.Description,
                ImageUrl = updateAboutDto.ImageUrl,
                VideoUrl = updateAboutDto.VideoUrl
            };
            await _aboutCollection.FindOneAndReplaceAsync(x => x.Id == updateAboutDto.Id, about);
        }
    }
}