using AkademiQMongoDb.DTOs.ChefDTOs;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using MongoDB.Driver;

namespace AkademiQMongoDb.Services.ChefServices
{
    public class ChefService : IChefService
    {
        private readonly IMongoCollection<Chef> _chefCollection;

        public ChefService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _chefCollection = database.GetCollection<Chef>("Chefs");
        }

        public async Task CreateChefAsync(CreateChefDto createChefDto)
        {
            var chef = new Chef
            {
                Name = createChefDto.Name,
                Title = createChefDto.Title,
                ImageUrl = createChefDto.ImageUrl,
                Status = createChefDto.Status
            };
            await _chefCollection.InsertOneAsync(chef);
        }

        public async Task DeleteChefAsync(string id)
        {
            await _chefCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultChefDto>> GetAllChefAsync()
        {
            var values = await _chefCollection.Find(x => true).ToListAsync();
            return values.Select(x => new ResultChefDto
            {
                Id = x.Id,
                Name = x.Name,
                Title = x.Title,
                ImageUrl = x.ImageUrl,
                Status = x.Status
            }).ToList();
        }

        public async Task<ResultChefDto> GetByIdChefAsync(string id)
        {
            var value = await _chefCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return new ResultChefDto
            {
                Id = value.Id,
                Name = value.Name,
                Title = value.Title,
                ImageUrl = value.ImageUrl,
                Status = value.Status
            };
        }

        public async Task UpdateChefAsync(UpdateChefDto updateChefDto)
        {
            var chef = new Chef
            {
                Id = updateChefDto.Id,
                Name = updateChefDto.Name,
                Title = updateChefDto.Title,
                ImageUrl = updateChefDto.ImageUrl,
                Status = updateChefDto.Status
            };
            await _chefCollection.FindOneAndReplaceAsync(x => x.Id == updateChefDto.Id, chef);
        }
    }
}