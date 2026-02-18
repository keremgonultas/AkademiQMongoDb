using AkademiQMongoDb.DTOs.BannerDtos;
using AkademiQMongoDb.Entities;
using AkademiQMongoDb.Settings;
using Mapster;
using MongoDB.Driver;
using MongoDB.Driver.Linq; // AsQueryable için gerekli

namespace AkademiQMongoDb.Services.BannerServices
{
    public class BannerService : IBannerService
    {
        private readonly IMongoCollection<Banner> _bannerCollection;

        public BannerService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _bannerCollection = database.GetCollection<Banner>(databaseSettings.BannerCollectionName);
        }

        public async Task CreateBannerAsync(CreateBannerDto createBannerDto)
        {
            var banner = createBannerDto.Adapt<Banner>();
            await _bannerCollection.InsertOneAsync(banner);
        }

        public async Task DeleteBannerAsync(string id)
        {
            await _bannerCollection.DeleteOneAsync(x => x.Id == id);
        }

        

        public async Task<List<ResultBannerDto>> GetAllAsync()
        {
            // ProductService'teki AsQueryable mantığının aynısı
            var banners = await _bannerCollection.AsQueryable().ToListAsync();
            return banners.Adapt<List<ResultBannerDto>>();
        }

        public async Task<UpdateBannerDto> GetByIdBannerAsync(string id)
        {
            var banner = await _bannerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return banner.Adapt<UpdateBannerDto>();
        }

        public async Task UpdateBannerAsync(UpdateBannerDto updateBannerDto)
        {
            var banner = updateBannerDto.Adapt<Banner>();
            // ProductService'teki gibi ID üzerinden nesne referansıyla eşleştirme
            await _bannerCollection.FindOneAndReplaceAsync(x => x.Id == banner.Id, banner);
        }
    }
}