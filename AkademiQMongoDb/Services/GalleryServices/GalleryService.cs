using AkademiQMongoDb.DTOs.GalleryDtos;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using MongoDB.Driver;

namespace AkademiQMongoDb.Services.GalleryServices
{
    public class GalleryService : IGalleryService
    {
        private readonly IMongoCollection<Gallery> _galleryCollection;

        public GalleryService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _galleryCollection = database.GetCollection<Gallery>("Galleries");
        }

        public async Task CreateAsync(CreateGalleryDto createGalleryDto)
        {
            var gallery = new Gallery
            {
                ImageUrl = createGalleryDto.ImageUrl
            };
            await _galleryCollection.InsertOneAsync(gallery);
        }

        public async Task DeleteAsync(string id)
        {
            await _galleryCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultGalleryDto>> GetAllAsync()
        {
            var values = await _galleryCollection.Find(x => true).ToListAsync();
            return values.Select(x => new ResultGalleryDto
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl
            }).ToList();
        }

        public async Task<UpdateGalleryDto> GetByIdAsync(string id)
        {
            var value = await _galleryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (value == null) return null;

            return new UpdateGalleryDto
            {
                Id = value.Id,
                ImageUrl = value.ImageUrl
            };
        }

        public async Task UpdateAsync(UpdateGalleryDto updateGalleryDto)
        {
            var gallery = new Gallery
            {
                Id = updateGalleryDto.Id,
                ImageUrl = updateGalleryDto.ImageUrl
            };
            await _galleryCollection.FindOneAndReplaceAsync(x => x.Id == updateGalleryDto.Id, gallery);
        }
    }
}